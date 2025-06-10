using ETLPipelineTool.Infrastructure.Loaders.Interfaces;

namespace ETLPipelineTool.Infrastructure.Loaders.Decorators
{
  public class LoggingLoaderDecorator(
    ILoader decoratedLoader,
    ILogger<LoggingLoaderDecorator> logger
  ) : ILoader
  {
    public async Task LoadAsync(
      EtlPipeline etlPipeline,
      IEnumerable<IDictionary<string, object>> transformedData,
      CancellationToken cancellationToken = default
    )
    {
      var stopwatch = Stopwatch.StartNew();
      var rowCount = transformedData.Count();

      logger.LogInformation(
        "Starting data load for pipeline {PipelineId} with {RowCount} rows",
        etlPipeline.Id,
        rowCount
      );

      try
      {
        await decoratedLoader.LoadAsync(etlPipeline, transformedData, cancellationToken);

        stopwatch.Stop();
        logger.LogInformation(
          "Successfully completed data load for pipeline {PipelineId} in {ElapsedMilliseconds}ms",
          etlPipeline.Id,
          stopwatch.ElapsedMilliseconds
        );
      }
      catch (Exception ex)
      {
        stopwatch.Stop();
        logger.LogError(
          ex,
          "Error during data load for pipeline {PipelineId} after {ElapsedMilliseconds}ms",
          etlPipeline.Id,
          stopwatch.ElapsedMilliseconds
        );
        throw;
      }
    }

    public void SetConnection(DbConnection connection)
    {
      decoratedLoader.SetConnection(connection);
    }
  }
}
