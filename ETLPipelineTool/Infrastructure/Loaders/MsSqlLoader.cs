using System.Data;
using System.Text;
using ETLPipelineTool.Application.Services.Interfaces;
using ETLPipelineTool.Infrastructure.Configurations;
using ETLPipelineTool.Infrastructure.Loaders.Interfaces;
using Microsoft.Data.SqlClient;

namespace ETLPipelineTool.Infrastructure.Loaders;

public class MssqlLoader(ILogger<MssqlLoader> logger, IConnectionManager connectionManager)
  : ILoader
{
  private DbConnection? _connection = null;
  private bool _externalConnection = false;

  public void SetConnection(DbConnection connection)
  {
    _connection = connection;
    _externalConnection = true;
  }

  public async Task LoadAsync(
    EtlPipeline etlPipeline,
    IEnumerable<IDictionary<string, object>> transformedData,
    CancellationToken cancellationToken = default
  )
  {
    var configuration = JsonSerializer.Deserialize<SqlTargetConfig>(
      etlPipeline.TargetConfigurationJson
    )!;

    if (string.IsNullOrEmpty(configuration.ConnectionString))
    {
      throw new BusinessException("SQL Server connection string is required");
    }

    var dataList = transformedData.ToList();
    if (dataList.Count == 0)
    {
      logger.LogWarning("No data to load");
      return;
    }

    var dataByTable = GroupDataByTable(dataList, etlPipeline);
    var tableLoadOrder = GetTableLoadOrder(dataByTable, etlPipeline);

    DbConnection conn;
    if (_externalConnection && _connection != null)
    {
      conn = _connection;
    }
    else
    {
      conn = await connectionManager.GetConnectionAsync(
        configuration.ConnectionString,
        cancellationToken
      );
    }

    try
    {
      if (configuration.CreateDatabaseIfNotExists)
      {
        await EnsureDatabaseExistsAsync((SqlConnection)conn, cancellationToken);
      }

      foreach (var tableName in tableLoadOrder)
      {
        if (!dataByTable.TryGetValue(tableName, out var tableData))
        {
          continue;
        }

        var targetTableName = GetTargetTableName(tableName, configuration);

        if (configuration.CreateTablesIfNotExist)
        {
          await EnsureTableExistsAsync(
            (SqlConnection)conn,
            targetTableName,
            tableData[0],
            configuration,
            etlPipeline,
            cancellationToken
          );
        }

        logger.LogInformation(
          "Loading {RowCount} rows into {TableName}",
          tableData.Count,
          targetTableName
        );

        if (configuration.UseBulkCopy)
        {
          await BulkLoadAsync(
            (SqlConnection)conn,
            targetTableName,
            tableData,
            configuration,
            cancellationToken
          );
        }
        else if (configuration.UseBatchInsert)
        {
          await BatchInsertAsync(
            (SqlConnection)conn,
            targetTableName,
            tableData,
            configuration,
            cancellationToken
          );
        }
        else
        {
          await RowByRowInsertAsync(
            (SqlConnection)conn,
            targetTableName,
            tableData,
            configuration,
            cancellationToken
          );
        }
      }
    }
    finally
    {
      if (!_externalConnection && conn != null)
      {
        // Do nothing
      }
    }
  }

  private static Dictionary<string, List<IDictionary<string, object>>> GroupDataByTable(
    List<IDictionary<string, object>> data,
    EtlPipeline etlPipeline
  )
  {
    if (etlPipeline.TableSchemas?.Count > 0)
    {
      if (etlPipeline.TableSchemas.Count == 1)
      {
        var tableName = etlPipeline.TableSchemas.First().TableName;
        return new Dictionary<string, List<IDictionary<string, object>>> { { tableName, data } };
      }

      var result = new Dictionary<string, List<IDictionary<string, object>>>();
      foreach (var schema in etlPipeline.TableSchemas)
      {
        var schemaColumns = schema.Columns.Select(x => x.ColumnName.ToLowerInvariant()).ToHashSet();
        var matchingRows = data.Where(row =>
            row.Keys.Count(k => schemaColumns.Contains(k.ToLowerInvariant()))
            >= row.Keys.Count * 0.7
          )
          .ToList();

        if (matchingRows.Count != 0)
        {
          result[schema.TableName] = matchingRows;
          data = [.. data.Except(matchingRows)];
        }
      }

      if (data.Count != 0)
      {
        var firstTableName = etlPipeline.TableSchemas.First().TableName;
        if (result.TryGetValue(firstTableName, out var value))
        {
          value.AddRange(data);
        }
        else
        {
          result[firstTableName] = data;
        }
      }

      return result;
    }
    else
    {
      throw new BusinessException("TableSchemas shouldn't be null");
    }

    // Default cases
    //const string tableNameField = "_TableName";
    //if (data.Count > 0 && data[0].ContainsKey(tableNameField))
    //{
    //  return data.GroupBy(row => row[tableNameField]?.ToString() ?? "DefaultTable")
    //    .ToDictionary(
    //      g => g.Key,
    //      g =>
    //        g.Select(row =>
    //          {
    //            var newRow = new Dictionary<string, object>(row);
    //            newRow.Remove(tableNameField);
    //            return newRow as IDictionary<string, object>;
    //          })
    //          .ToList()
    //    );
    //}

    //return new Dictionary<string, List<IDictionary<string, object>>> { { "DefaultTable", data } };
  }

  private static string GetTargetTableName(string sourceTableName, SqlTargetConfig config)
  {
    if (
      config.TableMapping != null
      && config.TableMapping.TryGetValue(sourceTableName, out var targetName)
    )
    {
      return $"{config.SchemaName}.{targetName}";
    }

    // Use source table name with schema
    return $"{config.SchemaName}.{sourceTableName}";
  }

  private async Task EnsureDatabaseExistsAsync(
    SqlConnection connection,
    CancellationToken cancellationToken
  )
  {
    var builder = new SqlConnectionStringBuilder(connection.ConnectionString);
    var databaseName = builder.InitialCatalog;

    builder.InitialCatalog = "master";

    await using var masterConn = new SqlConnection(builder.ConnectionString);
    await masterConn.OpenAsync(cancellationToken);

    var checkCmd = new SqlCommand(
      "SELECT COUNT(*) FROM sys.databases WHERE name = @dbName",
      masterConn
    );
    checkCmd.Parameters.AddWithValue("@dbName", databaseName);

    var exists = (int)await checkCmd.ExecuteScalarAsync(cancellationToken) > 0;

    if (!exists)
    {
      logger.LogInformation("Creating database {DatabaseName}", databaseName);

      var createCmd = new SqlCommand($"CREATE DATABASE [{databaseName}]", masterConn);

      await createCmd.ExecuteNonQueryAsync(cancellationToken);
    }
  }

  private async Task EnsureTableExistsAsync(
    SqlConnection connection,
    string tableName,
    IDictionary<string, object> sampleRow,
    SqlTargetConfig config,
    EtlPipeline etlPipeline,
    CancellationToken cancellationToken
  )
  {
    var tableExists = await CheckTableExistsAsync(connection, tableName, cancellationToken);

    if (!tableExists)
    {
      logger.LogInformation("Creating table {TableName}", tableName);

      var tableSchema =
        etlPipeline.TableSchemas.FirstOrDefault(x => x.TableName == tableName.Split('.')[^1])
        ?? throw new BusinessException($"Table schema not found for table {tableName}");

      var createTableSql = GenerateCreateTableStatement(tableName, sampleRow, tableSchema);

      var cmd = new SqlCommand(createTableSql, connection);
      if (config.CommandTimeout > 0)
      {
        cmd.CommandTimeout = config.CommandTimeout;
      }

      await cmd.ExecuteNonQueryAsync(cancellationToken);
    }
  }

  private static async Task<bool> CheckTableExistsAsync(
    SqlConnection connection,
    string tableName,
    CancellationToken cancellationToken
  )
  {
    var parts = tableName.Split('.');
    var schema = parts.Length > 1 ? parts[0] : "dbo";
    var table = parts.Length > 1 ? parts[1] : parts[0];

    var cmd = new SqlCommand(
      "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = @schema AND TABLE_NAME = @table",
      connection
    );
    cmd.Parameters.AddWithValue("@schema", schema);
    cmd.Parameters.AddWithValue("@table", table);

    var count = (int)await cmd.ExecuteScalarAsync(cancellationToken);
    return count > 0;
  }

  private static string GenerateCreateTableStatement(
    string tableName,
    IDictionary<string, object> sampleRow,
    TableSchema tableSchema
  )
  {
    var sb = new StringBuilder();
    sb.AppendLine($"CREATE TABLE {tableName} (");

    var columns = new List<string>();
    var primaryKeyColumns = tableSchema.Columns.Where(c => c.IsPrimaryKey).ToList();

    foreach (var column in tableSchema.Columns)
    {
      var sqlType = GetSqlTypeFromValue(sampleRow[column.ColumnName]);
      var nullable = column.IsNullable ? "NULL" : "NOT NULL";
      columns.Add($"[{column.ColumnName}] {sqlType} {nullable}");
    }

    if (primaryKeyColumns.Count != 0)
    {
      var pkColumns = string.Join(", ", primaryKeyColumns.Select(c => $"[{c.ColumnName}]"));
      columns.Add($"CONSTRAINT [PK_{tableName.Split('.')[^1]}] PRIMARY KEY ({pkColumns})");
    }

    sb.AppendLine(string.Join(",\n", columns));
    sb.AppendLine(")");

    foreach (var fk in tableSchema.ForeignKeys)
    {
      var fkColumns = string.Join(", ", fk.Columns.Select(c => $"[{c.ColumnName}]"));
      var principalColumns = string.Join(
        ", ",
        fk.PrincipalColumns.Select(c => $"[{c.PrincipalColumnName}]")
      );
      var principalTable = $"{tableName.Split('.')[0]}.{fk.PrincipalTable}";

      sb.AppendLine($"ALTER TABLE {tableName}");
      sb.AppendLine($"ADD CONSTRAINT [{fk.ConstraintName}] FOREIGN KEY ({fkColumns})");
      sb.AppendLine($"REFERENCES {principalTable} ({principalColumns})");
    }

    return sb.ToString();
  }

  private static string GetSqlTypeFromValue(object? value)
  {
    if (value == null)
    {
      return "nvarchar(max)";
    }

    return value switch
    {
      int or short or long or byte => "int",
      decimal or double or float => "decimal(18, 6)",
      DateTime => "datetime2",
      bool => "bit",
      Guid => "uniqueidentifier",
      string s => s.Length > 4000 ? "nvarchar(max)" : "nvarchar(4000)",
      _ => "nvarchar(max)",
    };
  }

  private async Task BulkLoadAsync(
    SqlConnection connection,
    string tableName,
    List<IDictionary<string, object>> data,
    SqlTargetConfig config,
    CancellationToken cancellationToken
  )
  {
    var dataTable = new DataTable();
    var firstRow = data[0];

    foreach (var key in firstRow.Keys)
    {
      dataTable.Columns.Add(key);
    }

    foreach (var row in data)
    {
      var dataRow = dataTable.NewRow();
      foreach (var kv in row)
      {
        dataRow[kv.Key] = kv.Value ?? DBNull.Value;
      }
      dataTable.Rows.Add(dataRow);
    }

    using var bulkCopy = new SqlBulkCopy(connection)
    {
      DestinationTableName = tableName,
      BatchSize = config.BatchSize > 0 ? config.BatchSize : 1000,
      BulkCopyTimeout = config.CommandTimeout > 0 ? config.CommandTimeout : 60,
    };

    foreach (DataColumn column in dataTable.Columns)
    {
      bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
    }

    await bulkCopy.WriteToServerAsync(dataTable, cancellationToken);
    logger.LogInformation("Bulk loaded {RowCount} rows into {TableName}", data.Count, tableName);
  }

  private async Task BatchInsertAsync(
    SqlConnection connection,
    string tableName,
    List<IDictionary<string, object>> data,
    SqlTargetConfig config,
    CancellationToken cancellationToken
  )
  {
    var batchSize = config.BatchSize > 0 ? config.BatchSize : 100;
    var batches = (int)Math.Ceiling(data.Count / (double)batchSize);

    for (int i = 0; i < batches; i++)
    {
      var batchData = data.Skip(i * batchSize).Take(batchSize).ToList();
      if (batchData.Count == 0)
      {
        continue;
      }

      var firstRow = batchData[0];
      var columns = firstRow.Keys.ToList();

      var sb = new StringBuilder();
      sb.Append($"INSERT INTO {tableName} (");
      sb.Append(string.Join(", ", columns.Select(c => $"[{c}]")));
      sb.AppendLine(") VALUES ");

      for (int j = 0; j < batchData.Count; j++)
      {
        sb.Append('(');
        sb.Append(string.Join(", ", columns.Select(c => $"@{c}{j}")));
        sb.Append(')');

        if (j < batchData.Count - 1)
        {
          sb.AppendLine(",");
        }
      }

      await using var cmd = new SqlCommand(sb.ToString(), connection);
      if (config.CommandTimeout > 0)
      {
        cmd.CommandTimeout = config.CommandTimeout;
      }

      for (int j = 0; j < batchData.Count; j++)
      {
        var row = batchData[j];
        foreach (var col in columns)
        {
          cmd.Parameters.AddWithValue($"@{col}{j}", row[col] ?? DBNull.Value);
        }
      }

      await cmd.ExecuteNonQueryAsync(cancellationToken);
    }

    logger.LogInformation("Batch inserted {RowCount} rows into {TableName}", data.Count, tableName);
  }

  private async Task RowByRowInsertAsync(
    SqlConnection connection,
    string tableName,
    List<IDictionary<string, object>> data,
    SqlTargetConfig config,
    CancellationToken cancellationToken
  )
  {
    int rowsProcessed = 0;

    foreach (var row in data)
    {
      var cols = string.Join(", ", row.Keys.Select(k => $"[{k}]"));
      var pars = string.Join(", ", row.Keys.Select(k => $"@{k}"));
      var sql = $"INSERT INTO {tableName} ({cols}) VALUES ({pars})";

      await using var cmd = new SqlCommand(sql, connection);
      if (config.CommandTimeout > 0)
      {
        cmd.CommandTimeout = config.CommandTimeout;
      }

      foreach (var kv in row)
      {
        cmd.Parameters.AddWithValue($"@{kv.Key}", kv.Value ?? DBNull.Value);
      }

      await cmd.ExecuteNonQueryAsync(cancellationToken);
      rowsProcessed++;

      if (rowsProcessed % 100 == 0)
      {
        logger.LogDebug("Processed {RowCount} rows", rowsProcessed);
      }
    }

    logger.LogInformation("Inserted {RowCount} rows into {TableName}", rowsProcessed, tableName);
  }

  private List<string> GetTableLoadOrder(
    Dictionary<string, List<IDictionary<string, object>>> dataByTable,
    EtlPipeline etlPipeline
  )
  {
    var graph = new Dictionary<string, HashSet<string>>();
    var inDegree = new Dictionary<string, int>();

    foreach (var tableName in dataByTable.Keys)
    {
      graph[tableName] = [];
      inDegree[tableName] = 0;
    }

    // Build dependency graph
    foreach (var tableSchema in etlPipeline.TableSchemas)
    {
      var tableName = tableSchema.TableName;
      foreach (
        var fk in tableSchema.ForeignKeys.Where(fk => dataByTable.ContainsKey(fk.PrincipalTable))
      )
      {
        graph[fk.PrincipalTable].Add(tableName);
        inDegree[tableName]++;
      }
    }

    // Topological sort
    var result = new List<string>();
    var queue = new Queue<string>(inDegree.Where(x => x.Value == 0).Select(x => x.Key));

    while (queue.Count > 0)
    {
      var current = queue.Dequeue();
      result.Add(current);

      foreach (var dependent in graph[current])
      {
        inDegree[dependent]--;
        if (inDegree[dependent] == 0)
        {
          queue.Enqueue(dependent);
        }
      }
    }

    // Add any remaining tables (should not happen in a valid schema)
    foreach (var table in inDegree.Where(x => x.Value > 0).Select(x => x.Key))
    {
      logger.LogWarning("Table {TableName} has unresolved dependencies", table);
      result.Add(table);
    }

    return result;
  }
}
