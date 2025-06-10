using ETLPipelineTool.Infrastructure.Transformers.Interfaces;

namespace ETLPipelineTool.Infrastructure.Transformers.Decorators
{
  public class LoggingTransformerDecorator(
    ITransformer inner,
    ILogger<LoggingTransformerDecorator> logger
  ) : ITransformer
  {
    public async Task<IEnumerable<IDictionary<string, object>>> TransformAsync(
      EtlPipeline etlPipeline,
      IEnumerable<IDictionary<string, object>> rawData,
      CancellationToken cancellationToken = default
    )
    {
      var stopwatch = Stopwatch.StartNew();
      var rowCount = rawData?.Count() ?? 0;

      logger.LogInformation(
        "Starting transformation of {RowCount} rows for pipeline {PipelineId} with {MappingCount} field mappings",
        rowCount,
        etlPipeline.Id,
        etlPipeline.FieldMappings?.Count ?? 0
      );

      try
      {
        var result = await inner.TransformAsync(etlPipeline, rawData!, cancellationToken);

        stopwatch.Stop();
        var resultCount = result?.Count() ?? 0;

        logger.LogInformation(
          "Completed transformation of {RowCount} rows to {ResultCount} results in {ElapsedMs}ms for pipeline {PipelineId}",
          rowCount,
          resultCount,
          stopwatch.ElapsedMilliseconds,
          etlPipeline.Id
        );

        return result!;
      }
      catch (Exception ex)
      {
        stopwatch.Stop();

        logger.LogError(
          ex,
          "Error transforming {RowCount} rows for pipeline {PipelineId} after {ElapsedMs}ms",
          rowCount,
          etlPipeline.Id,
          stopwatch.ElapsedMilliseconds
        );

        throw;
      }
    }
  }
}
