using ETLPipelineTool.Domain.ValueObjects;
using ETLPipelineTool.Infrastructure.Extractors.Interfaces;

namespace ETLPipelineTool.Infrastructure.Extractors.Decorators
{
  public class LoggingExtractorDecorator(
    IExtractor inner,
    ILogger<LoggingExtractorDecorator> logger
  ) : IExtractor
  {
    public IAsyncEnumerable<TableData> ExtractAsync(
      EtlPipeline etlPipeline,
      CancellationToken cancellationToken = default
    )
    {
      var stopwatch = Stopwatch.StartNew();

      logger.LogInformation("Starting data extraction for pipeline {PipelineId}", etlPipeline.Id);

      try
      {
        var result = inner.ExtractAsync(etlPipeline, cancellationToken);

        _ = LogCompletionAsync(result, etlPipeline, stopwatch, cancellationToken);

        return result;
      }
      catch (Exception ex)
      {
        stopwatch.Stop();
        logger.LogError(
          ex,
          "Error during data extraction for pipeline {PipelineId} after {ElapsedMilliseconds}ms",
          etlPipeline.Id,
          stopwatch.ElapsedMilliseconds
        );
        throw;
      }
    }

    private async Task LogCompletionAsync(
      IAsyncEnumerable<TableData> tables,
      EtlPipeline etlPipeline,
      Stopwatch stopwatch,
      CancellationToken cancellationToken
    )
    {
      try
      {
        int tableCount = 0;
        int totalRowCount = 0;

        await foreach (var table in tables.WithCancellation(cancellationToken))
        {
          tableCount++;
          logger.LogInformation("Extracting table {Table}", table.TableName);

          int rowCount = 0;
          await foreach (var _ in table.Rows.WithCancellation(cancellationToken))
          {
            rowCount++;
          }

          totalRowCount += rowCount;
          logger.LogInformation(
            "Extracted {RowCount} rows from {Table}",
            rowCount,
            table.TableName
          );
        }

        stopwatch.Stop();
        logger.LogInformation(
          "Successfully completed data extraction for pipeline {PipelineId}: {TableCount} tables, {RowCount} rows in {ElapsedMilliseconds}ms",
          etlPipeline.Id,
          tableCount,
          totalRowCount,
          stopwatch.ElapsedMilliseconds
        );
      }
      catch (Exception ex)
      {
        stopwatch.Stop();
        logger.LogError(
          ex,
          "Error during data extraction for pipeline {PipelineId} after {ElapsedMilliseconds}ms",
          etlPipeline.Id,
          stopwatch.ElapsedMilliseconds
        );
      }
    }

    public void SetConnection(DbConnection connection)
    {
      inner.SetConnection(connection);
    }
  }
}
