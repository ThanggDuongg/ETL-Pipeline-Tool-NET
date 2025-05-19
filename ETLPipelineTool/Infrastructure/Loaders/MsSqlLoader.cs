using ETLPipelineTool.Infrastructure.Loaders.Interfaces;
using Microsoft.Data.SqlClient;

namespace ETLPipelineTool.Infrastructure.Loaders;

public class MsSqlLoader : ILoader
{
    public async Task LoadAsync(
        EtlPipeline etlPipeline,
        IEnumerable<IDictionary<string, object>> transformedData,
        CancellationToken cancellationToken = default
    )
    {
        var configuration = JsonSerializer.Deserialize<SqlTargetConfig>(
            etlPipeline.TargetConfigurationJson
        )!;

        using var conn = new SqlConnection(configuration.ConnectionString);
        await conn.OpenAsync(cancellationToken);

        foreach (var row in transformedData)
        {
            var cols = string.Join(", ", row.Keys);
            var pars = string.Join(", ", row.Keys.Select(k => "@" + k));
            var sql = $"INSERT INTO {configuration.TableName} ({cols}) VALUES ({pars})";

            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            foreach (var kv in row)
                cmd.Parameters.AddWithValue("@" + kv.Key, kv.Value ?? DBNull.Value);

            await cmd.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    private sealed class SqlTargetConfig
    {
        public string ConnectionString { get; set; } = default!;
        public string TableName { get; set; } = default!;
    }
}
