using System.Runtime.CompilerServices;
using ETLPipelineTool.Domain.ValueObjects;
using ETLPipelineTool.Infrastructure.Extractors.Interfaces;
using Microsoft.Data.SqlClient;

namespace ETLPipelineTool.Infrastructure.Extractors
{
  public class MssqlExtractor : IExtractor
  {
    private sealed class SqlSourceConfig
    {
      public string ConnectionString { get; init; } = default!;
      public string? Query { get; init; } = null;
      public ICollection<string>? Tables { get; init; } = null;
    }

    public async IAsyncEnumerable<TableData> ExtractAsync(
      EtlPipeline etlPipeline,
      [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
      var cfg = JsonSerializer.Deserialize<SqlSourceConfig>(etlPipeline.SourceConfigurationJson)!;

      await using var conn = new SqlConnection(cfg.ConnectionString);
      await conn.OpenAsync(cancellationToken);

      if (!string.IsNullOrEmpty(cfg.Query))
      {
        yield return new TableData(
          TableName: "QueryResult",
          Rows: StreamRows(conn, cfg.Query, cancellationToken)
        );
        yield break;
      }

      var tables = cfg.Tables?.ToList() ?? await GetAllTableNamesAsync(conn, cancellationToken);

      foreach (var tbl in tables)
      {
        var sql = $"SELECT * FROM {tbl}";
        yield return new TableData(tbl, StreamRows(conn, sql, cancellationToken));
      }
    }

    private static async Task<List<string>> GetAllTableNamesAsync(
      SqlConnection conn,
      CancellationToken cancellationToken
    )
    {
      var result = new List<string>();
      var cmd = conn.CreateCommand();
      cmd.CommandText =
        @"
            SELECT TABLE_SCHEMA + '.' + TABLE_NAME 
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_TYPE = 'BASE TABLE'";
      await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
      while (await reader.ReadAsync(cancellationToken))
      {
        result.Add(reader.GetString(0));
      }
      return result;
    }

    private static async IAsyncEnumerable<IDictionary<string, object>> StreamRows(
      SqlConnection conn,
      string sql,
      [EnumeratorCancellation] CancellationToken cancellationToken
    )
    {
      await using var cmd = conn.CreateCommand();
      cmd.CommandText = sql;
      await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
      while (await reader.ReadAsync(cancellationToken))
      {
        var dict = new Dictionary<string, object>(reader.FieldCount);
        for (int i = 0; i < reader.FieldCount; i++)
          dict[reader.GetName(i)] =
            (await reader.IsDBNullAsync(i, cancellationToken)) ? DBNull.Value : reader.GetValue(i);
        yield return dict;
      }
    }
  }
}
