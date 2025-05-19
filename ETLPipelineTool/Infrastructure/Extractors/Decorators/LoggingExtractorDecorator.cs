using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using ETLPipelineTool.Domain.ValueObjects;
using ETLPipelineTool.Infrastructure.Extractors.Interfaces;
using Polly;
using Polly.Retry;

namespace ETLPipelineTool.Infrastructure.Extractors.Decorators
{
    // Study Purpose
    public class LoggingExtractorDecorator : IExtractor
    {
        private readonly IExtractor _inner;
        private readonly ILogger<LoggingExtractorDecorator> _logger;
        private readonly RetryPolicy _retryPolicy;

        private static readonly Meter _meter = new("ETL.Process", "1.0.0");
        private static readonly Counter<long> _tableCounter = _meter.CreateCounter<long>(
            "etl.extract.tables"
        );
        private static readonly Counter<long> _rowCounter = _meter.CreateCounter<long>(
            "etl.extract.rows"
        );
        private static readonly Histogram<double> _latencyHistogram =
            _meter.CreateHistogram<double>("etl.extract.latency-ms");

        public LoggingExtractorDecorator(
            IExtractor inner,
            ILogger<LoggingExtractorDecorator> logger
        )
        {
            _inner = inner;
            _logger = logger;

            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetry(
                    retryCount: 3,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    onRetry: (ex, delay, attempt, _) =>
                        _logger.LogWarning(
                            ex,
                            "Retry {Attempt} after {Delay} due to error",
                            attempt,
                            delay
                        )
                );
        }

        public async IAsyncEnumerable<TableData> ExtractAsync(
            EtlPipeline etlPipeline,
            [EnumeratorCancellation] CancellationToken cancellationToken = default
        )
        {
            // Retry the entire enumeration if it fails
            var enumerated = _retryPolicy.Execute(
                () => _inner.ExtractAsync(etlPipeline, cancellationToken)
            );

            await foreach (var table in enumerated.WithCancellation(cancellationToken))
            {
                _logger.LogInformation("Start extracting table {Table}", table.TableName);
                _tableCounter.Add(1, new KeyValuePair<string, object?>("table", table.TableName));

                async IAsyncEnumerable<IDictionary<string, object>> WrappedRows()
                {
                    var rowCount = 0;
                    var sw = Stopwatch.StartNew();

                    await foreach (var row in table.Rows.WithCancellation(cancellationToken))
                    {
                        rowCount++;
                        yield return row;
                    }

                    sw.Stop();

                    _rowCounter.Add(
                        rowCount,
                        new KeyValuePair<string, object?>("table", table.TableName)
                    );
                    _latencyHistogram.Record(
                        sw.Elapsed.TotalMilliseconds,
                        new KeyValuePair<string, object?>("table", table.TableName)
                    );

                    _logger.LogInformation(
                        "Extracted {RowCount} rows from {Table} in {ElapsedMs} ms",
                        rowCount,
                        table.TableName,
                        sw.Elapsed.TotalMilliseconds
                    );
                }

                yield return new TableData(table.TableName, WrappedRows());
            }
        }
    }
}
