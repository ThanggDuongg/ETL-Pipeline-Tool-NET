using ETLPipelineTool.Infrastructure.Configurations;
using ETLPipelineTool.Infrastructure.Extractors.Interfaces;
using Microsoft.Data.SqlClient;

namespace ETLPipelineTool.Infrastructure.Extractors
{
  public class MssqlSchemaExtractor(ILogger<MssqlSchemaExtractor> logger) : ISchemaExtractor
  {
    public async Task<TableSchema> GetTableSchemaAsync(
      string configurationJson,
      string tableName,
      CancellationToken cancellationToken = default
    )
    {
      await using var conn = await CreateAndOpenConnectionAsync(
        configurationJson,
        cancellationToken
      );
      return await GetTableSchemaWithConnectionAsync(conn, tableName, cancellationToken);
    }

    public async Task<ICollection<string>> GetTableNamesAsync(
      string configurationJson,
      CancellationToken cancellationToken = default
    )
    {
      await using var conn = await CreateAndOpenConnectionAsync(
        configurationJson,
        cancellationToken
      );
      return await GetTableNamesWithConnectionAsync(conn, cancellationToken);
    }

    public async Task<ICollection<TableSchema>> GetTableSchemasAsync(
      string configurationJson,
      ICollection<string> tableNames,
      CancellationToken cancellationToken = default
    )
    {
      var result = new List<TableSchema>();

      await using var conn = await CreateAndOpenConnectionAsync(
        configurationJson,
        cancellationToken
      );

      foreach (var tableName in tableNames)
      {
        try
        {
          // N + 1, but accepted due to business
          var schema = await GetTableSchemaWithConnectionAsync(conn, tableName, cancellationToken);
          result.Add(schema);
        }
        catch (Exception ex)
        {
          LogError(ex, $"Error getting schema for table {tableName}");
          throw new BusinessException($"Error getting schema for table {tableName}");
        }
      }

      return result;
    }

    private static async Task<SqlConnection> CreateAndOpenConnectionAsync(
      string configurationJson,
      CancellationToken cancellationToken
    )
    {
      var cfg = JsonSerializer.Deserialize<SqlSourceConfig>(configurationJson)!;
      var conn = new SqlConnection(cfg.ConnectionString);
      await conn.OpenAsync(cancellationToken);
      return conn;
    }

    private static async Task<ICollection<string>> GetTableNamesWithConnectionAsync(
      SqlConnection connection,
      CancellationToken cancellationToken
    )
    {
      var result = new List<string>();
      var cmd = connection.CreateCommand();
      cmd.CommandText =
        @"
        SELECT SCHEMA_NAME(schema_id) + '.' + name 
        FROM sys.tables 
        ORDER BY SCHEMA_NAME(schema_id), name";

      await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
      while (await reader.ReadAsync(cancellationToken))
      {
        result.Add(reader.GetString(0));
      }

      return result;
    }

    private static async Task<TableSchema> GetTableSchemaWithConnectionAsync(
      SqlConnection connection,
      string tableName,
      CancellationToken cancellationToken = default
    )
    {
      var (schemaName, tableNameOnly) = ParseTableName(tableName);

      var schema = new TableSchema
      {
        TableName = tableName,
        Columns = [],
        ForeignKeys = [],
      };

      await GetColumnsAsync(connection, schemaName, tableNameOnly, schema, cancellationToken);
      await GetForeignKeysAsync(connection, schemaName, tableNameOnly, schema, cancellationToken);

      return schema;
    }

    private static (string SchemaName, string TableNameOnly) ParseTableName(string tableName)
    {
      var parts = tableName.Split('.');
      return parts.Length > 1 ? (parts[0], parts[1]) : ("dbo", tableName);
    }

    private static async Task GetColumnsAsync(
      SqlConnection conn,
      string schemaName,
      string tableNameOnly,
      TableSchema tableSchema,
      CancellationToken cancellationToken
    )
    {
      var cmd = CreateColumnInfoCommand(conn, schemaName, tableNameOnly);
      await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

      while (await reader.ReadAsync(cancellationToken))
      {
        tableSchema.Columns.Add(CreateColumnSchemaFromReader(reader));
      }
    }

    private static SqlCommand CreateColumnInfoCommand(
      SqlConnection conn,
      string schemaName,
      string tableNameOnly
    )
    {
      var cmd = conn.CreateCommand();
      cmd.CommandText =
        @"
        SELECT 
            c.name AS ColumnName,
            t.name AS DataType,
            c.is_nullable AS IsNullable,
            c.max_length AS MaxLength,
            CASE WHEN pk.column_id IS NOT NULL THEN 1 ELSE 0 END AS IsPrimaryKey
        FROM sys.columns c
        INNER JOIN sys.types t ON c.user_type_id = t.user_type_id
        INNER JOIN sys.tables tbl ON c.object_id = tbl.object_id
        INNER JOIN sys.schemas s ON tbl.schema_id = s.schema_id
        LEFT JOIN (
            SELECT ic.column_id, ic.object_id
            FROM sys.index_columns ic
            INNER JOIN sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id
            WHERE i.is_primary_key = 1
        ) pk ON c.column_id = pk.column_id AND c.object_id = pk.object_id
        WHERE s.name = @SchemaName AND tbl.name = @TableName
        ORDER BY c.column_id";

      cmd.Parameters.AddWithValue("@SchemaName", schemaName);
      cmd.Parameters.AddWithValue("@TableName", tableNameOnly);

      return cmd;
    }

    private static ColumnSchema CreateColumnSchemaFromReader(DbDataReader reader)
    {
      return new ColumnSchema
      {
        ColumnName = reader.GetString(0),
        DataType = reader.GetString(1),
        IsNullable = reader.GetBoolean(2),
        MaxLength = reader.GetInt16(3),
        IsPrimaryKey = reader.GetInt32(4) == 1,
      };
    }

    private static async Task GetForeignKeysAsync(
      SqlConnection conn,
      string schemaName,
      string tableNameOnly,
      TableSchema tableSchema,
      CancellationToken cancellationToken
    )
    {
      var cmd = CreateForeignKeyCommand(conn, schemaName, tableNameOnly);
      var fkDict = new Dictionary<string, (string, List<(string, string)>)>();

      await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
      while (await reader.ReadAsync(cancellationToken))
      {
        ProcessForeignKeyRow(reader, fkDict);
      }

      BuildForeignKeySchemas(fkDict, tableSchema);
    }

    private static SqlCommand CreateForeignKeyCommand(
      SqlConnection conn,
      string schemaName,
      string tableNameOnly
    )
    {
      var cmd = conn.CreateCommand();
      cmd.CommandText =
        @"
        SELECT 
            fk.name AS ConstraintName,
            SCHEMA_NAME(tp.schema_id) + '.' + tp.name AS PrincipalTable,
            cp.name AS ColumnName,
            ctp.name AS PrincipalColumnName
        FROM sys.foreign_keys fk
        INNER JOIN sys.tables t ON fk.parent_object_id = t.object_id
        INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
        INNER JOIN sys.tables tp ON fk.referenced_object_id = tp.object_id
        INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
        INNER JOIN sys.columns cp ON fkc.parent_column_id = cp.column_id AND fkc.parent_object_id = cp.object_id
        INNER JOIN sys.columns ctp ON fkc.referenced_column_id = ctp.column_id AND fkc.referenced_object_id = ctp.object_id
        WHERE s.name = @SchemaName AND t.name = @TableName
        ORDER BY fk.name, fkc.constraint_column_id";

      cmd.Parameters.AddWithValue("@SchemaName", schemaName);
      cmd.Parameters.AddWithValue("@TableName", tableNameOnly);

      return cmd;
    }

    private static void ProcessForeignKeyRow(
      DbDataReader reader,
      Dictionary<string, (string, List<(string, string)>)> fkDict
    )
    {
      var constraintName = reader.GetString(0);
      var principalTable = reader.GetString(1);
      var columnName = reader.GetString(2);
      var principalColumnName = reader.GetString(3);

      if (!fkDict.TryGetValue(constraintName, out var value))
      {
        value = (principalTable, new List<(string, string)>());
        fkDict[constraintName] = value;
      }

      value.Item2.Add((columnName, principalColumnName));
    }

    private static void BuildForeignKeySchemas(
      Dictionary<string, (string, List<(string, string)>)> fkDict,
      TableSchema tableSchema
    )
    {
      foreach (var fk in fkDict)
      {
        var foreignKey = new ForeignKeySchema
        {
          ConstraintName = fk.Key,
          PrincipalTable = fk.Value.Item1,
          Columns = [],
          PrincipalColumns = [],
        };

        foreach (var (column, principalColumn) in fk.Value.Item2)
        {
          foreignKey.Columns.Add(new ForeignKeyColumn { ColumnName = column });
          foreignKey.PrincipalColumns.Add(
            new ForeignKeyPrincipalColumn { PrincipalColumnName = principalColumn }
          );
        }

        tableSchema.ForeignKeys.Add(foreignKey);
      }
    }

    private void LogError(Exception ex, string message)
    {
      logger.LogError(ex, message);
    }
  }
}
