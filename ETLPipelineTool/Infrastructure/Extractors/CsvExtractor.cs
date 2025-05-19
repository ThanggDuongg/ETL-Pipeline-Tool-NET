using System.Globalization;
using System.Runtime.CompilerServices;
using CsvHelper;
using ETLPipelineTool.Domain.ValueObjects;
using ETLPipelineTool.Infrastructure.Extractors.Interfaces;

namespace ETLPipelineTool.Infrastructure.Extractors
{
    public class CsvExtractor : IExtractor
    {
        private sealed class CsvSourceConfig
        {
            public string Path { get; init; } = default!;
            public bool HasHeader { get; init; } = true;
            public string Delimiter { get; init; } = ",";
        }

        public async IAsyncEnumerable<TableData> ExtractAsync(
            EtlPipeline etlPipeline,
            [EnumeratorCancellation] CancellationToken cancellationToken = default
        )
        {
            var cfg = JsonSerializer.Deserialize<CsvSourceConfig>(
                etlPipeline.SourceConfigurationJson
            )!;

            var files = Directory.Exists(cfg.Path)
                ? Directory.EnumerateFiles(cfg.Path, "*.csv")
                : [cfg.Path];

            foreach (var file in files)
            {
                var tableName = Path.GetFileNameWithoutExtension(file);
                yield return await Task.FromResult(
                    new TableData(tableName, StreamCsvRows(file, cfg, cancellationToken))
                );
            }
        }

        private static async IAsyncEnumerable<IDictionary<string, object>> StreamCsvRows(
            string filePath,
            CsvSourceConfig cfg,
            [EnumeratorCancellation] CancellationToken ct
        )
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(
                reader,
                new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = cfg.HasHeader,
                    Delimiter = cfg.Delimiter,
                }
            );

            await foreach (var rec in csv.GetRecordsAsync<dynamic>(ct))
            {
                var dict = (IDictionary<string, object>)rec;
                yield return new Dictionary<string, object>(dict);
            }
        }
    }
}
