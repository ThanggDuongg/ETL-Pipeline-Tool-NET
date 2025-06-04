using System.Runtime.CompilerServices;
using ETLPipelineTool.Domain.ValueObjects;
using ETLPipelineTool.Infrastructure.Configurations;
using ETLPipelineTool.Infrastructure.Extractors.Interfaces;
using Microsoft.Data.SqlClient;

namespace ETLPipelineTool.Infrastructure.Extractors
{
  public class MssqlExtractor(ILogger<MssqlExtractor> logger) : IExtractor
  {
    public async IAsyncEnumerable<TableData> ExtractAsync(
      EtlPipeline etlPipeline,
      [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
      var cfg = JsonSerializer.Deserialize<SqlSourceConfig>(etlPipeline.SourceConfigurationJson)!;

      await using var conn = new SqlConnection(cfg.ConnectionString);
      await conn.OpenAsync(cancellationToken);

      logger.LogInformation("Connected to SQL Server database");

      if (!string.IsNullOrEmpty(cfg.Query))
      {
        logger.LogInformation("Executing custom query");
        yield return new TableData(
          TableName: "QueryResult",
          Rows: StreamRows(
            conn,
            cfg.Query,
            cfg.CommandTimeout,
            cfg.BatchSize,
            cfg.UseColumnMetadata,
            cancellationToken
          )
        );
        yield break;
      }

      var tables = etlPipeline.TableSchemas.Select(s => s.TableName).ToList();

      if (tables.Count == 0)
      {
        throw new BusinessException("No table schemas found. Please sync schemas first.");
      }

      logger.LogInformation("Found {TableCount} tables with schemas to extract", tables.Count);

      foreach (var tbl in tables)
      {
        var schema = etlPipeline.TableSchemas.First(s =>
          s.TableName.Equals(tbl, StringComparison.OrdinalIgnoreCase)
        );

        logger.LogInformation("Extracting data from table {TableName}", tbl);

        var columnNames = string.Join(", ", schema.Columns.Select(c => $"[{c.ColumnName}]"));
        var sql = $"SELECT {columnNames} FROM {tbl}";

        yield return new TableData(
          tbl,
          StreamRows(
            conn,
            sql,
            cfg.CommandTimeout,
            cfg.BatchSize,
            cfg.UseColumnMetadata,
            cancellationToken
          )
        );
      }
    }

    private static async IAsyncEnumerable<IDictionary<string, object>> StreamRows(
      SqlConnection conn,
      string sql,
      int? commandTimeout,
      int? batchSize,
      bool useColumnMetadata,
      [EnumeratorCancellation] CancellationToken cancellationToken
    )
    {
      await using var cmd = conn.CreateCommand();
      cmd.CommandText = sql;

      if (commandTimeout.HasValue)
      {
        cmd.CommandTimeout = commandTimeout.Value;
      }

      await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

      var columnNames = new string[reader.FieldCount];
      var columnTypes = new Type[reader.FieldCount];
      var sqlColumnTypes = new string[reader.FieldCount];

      for (int i = 0; i < reader.FieldCount; i++)
      {
        columnNames[i] = reader.GetName(i);
        if (useColumnMetadata)
        {
          columnTypes[i] = reader.GetFieldType(i);
          sqlColumnTypes[i] = reader.GetDataTypeName(i);
        }
      }

      int rowCount = 0;
      int currentBatchSize = batchSize ?? 1000;

      while (await reader.ReadAsync(cancellationToken))
      {
        var dict = new Dictionary<string, object>(reader.FieldCount);

        for (int i = 0; i < reader.FieldCount; i++)
        {
          if (await reader.IsDBNullAsync(i, cancellationToken))
          {
            dict[columnNames[i]] = DBNull.Value;
            continue;
          }

          if (useColumnMetadata)
          {
            dict[columnNames[i]] = columnTypes[i].Name switch
            {
              nameof(DateTime) => reader.GetDateTime(i),
              nameof(DateTimeOffset) => reader.GetDateTimeOffset(i),
              nameof(Decimal) => reader.GetDecimal(i),
              nameof(Double) => reader.GetDouble(i),
              nameof(Single) => reader.GetFloat(i),
              nameof(Guid) => reader.GetGuid(i),
              nameof(Boolean) => reader.GetBoolean(i),
              nameof(Byte) => reader.GetByte(i),
              nameof(Char) => reader.GetChar(i),
              nameof(Int16) => reader.GetInt16(i),
              nameof(Int32) => reader.GetInt32(i),
              nameof(Int64) => reader.GetInt64(i),
              _ => sqlColumnTypes[i].ToLowerInvariant() switch
              {
                "geography" or "geometry" or "hierarchyid" or "xml" => reader.GetValue(i).ToString()
                  ?? string.Empty,
                "money" or "smallmoney" => reader.GetDecimal(i),
                "time" => reader.GetTimeSpan(i),
                "date" => reader.GetDateTime(i).Date,
                _ => reader.GetValue(i),
              },
            };
          }
          else
          {
            dict[columnNames[i]] = reader.GetValue(i);
          }
        }

        yield return dict;

        rowCount++;
        if (rowCount % currentBatchSize == 0)
        {
          await Task.Delay(1, cancellationToken);
        }
      }
    }
  }
}
