using ETLPipelineTool.Application.Services.Interfaces;
using ETLPipelineTool.Domain.ValueObjects;
using ETLPipelineTool.Infrastructure.Configurations;
using ETLPipelineTool.Infrastructure.Extractors.Interfaces;
using ETLPipelineTool.Infrastructure.Loaders.Interfaces;
using ETLPipelineTool.Infrastructure.Transformers.Interfaces;
using Polly;

namespace ETLPipelineTool.Application.Services
{
  public class PipelineOrchestrator(
    IExtractorFactory extractorFactory,
    ITransformer transformer,
    ILoaderFactory loaderFactory,
    IEtlExecutionLogRepository executionLogRepository,
    IConnectionManager connectionManager,
    ILogger<PipelineOrchestrator> logger
  ) : IPipelineOrchestrator
  {
    public async Task<ExecutionResult> ExecutePipelineAsync(
      EtlPipeline pipeline,
      CancellationToken cancellationToken = default
    )
    {
      ArgumentNullException.ThrowIfNull(pipeline);

      var executionLog = new EtlExecutionLog
      {
        EtlPipelineId = pipeline.Id,
        StartTime = DateTime.UtcNow,
        Status = EtlExecutionStatus.Running,
      };

      await executionLogRepository.AddAsync(executionLog, cancellationToken);

      var stopwatch = Stopwatch.StartNew();
      var result = new ExecutionResult(pipeline.Id);

      try
      {
        logger.LogInformation(
          "Starting ETL pipeline execution: {PipelineId} - {PipelineName}",
          pipeline.Id,
          pipeline.Name
        );

        var retryPolicy = Policy
          .Handle<Exception>(ex => ex is not OperationCanceledException)
          .WaitAndRetryAsync(
            3,
            attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
            (ex, timeSpan, retryCount, context) =>
            {
              logger.LogWarning(
                ex,
                "Error during pipeline execution, retrying ({RetryCount}/3) after {RetryTimeSpan}s",
                retryCount,
                timeSpan.TotalSeconds
              );
            }
          );

        await retryPolicy.ExecuteAsync(async () =>
        {
          var sourceConfig = JsonSerializer.Deserialize<SqlSourceConfig>(
            pipeline.SourceConfigurationJson
          );
          var targetConfig = JsonSerializer.Deserialize<SqlTargetConfig>(
            pipeline.TargetConfigurationJson
          );

          if (sourceConfig == null || targetConfig == null)
          {
            throw new InvalidOperationException(
              "Failed to deserialize source or target configuration"
            );
          }

          var sourceConnection = await connectionManager.GetConnectionAsync(
            sourceConfig.ConnectionString,
            cancellationToken
          );

          bool isSameConnection = sourceConfig.ConnectionString.Equals(
            targetConfig.ConnectionString,
            StringComparison.OrdinalIgnoreCase
          );

          var targetConnection = isSameConnection
            ? sourceConnection
            : await connectionManager.GetConnectionAsync(
              targetConfig.ConnectionString,
              cancellationToken
            );

          // Extractor
          logger.LogInformation("Starting data extraction from source");
          var extractor = extractorFactory.Create(pipeline.SourceType);
          extractor.SetConnection(sourceConnection);

          var extractedData = new List<IDictionary<string, object>>();

          await foreach (var tableData in extractor.ExtractAsync(pipeline, cancellationToken))
          {
            var rowsList = new List<IDictionary<string, object>>();
            await foreach (var row in tableData.Rows.WithCancellation(cancellationToken))
            {
              rowsList.Add(row);
            }

            extractedData.AddRange(rowsList);
            result.ExtractedRowCount += rowsList.Count;
          }

          logger.LogInformation("Extracted {RowCount} rows from source", result.ExtractedRowCount);

          // Transformer
          logger.LogInformation("Starting data transformation");
          var transformedData = await transformer.TransformAsync(
            pipeline,
            extractedData,
            cancellationToken
          );
          result.TransformedRowCount = transformedData.Count();

          logger.LogInformation("Transformed data: {RowCount} rows", result.TransformedRowCount);

          // Loader
          logger.LogInformation("Starting data loading to target");
          var loader = loaderFactory.Create(pipeline.TargetType);
          loader.SetConnection(targetConnection);

          await loader.LoadAsync(pipeline, transformedData, cancellationToken);
          result.LoadedRowCount = transformedData.Count();

          logger.LogInformation("Loaded {RowCount} rows to target", result.LoadedRowCount);
        });

        result.Success = true;
        executionLog.Status = EtlExecutionStatus.Completed;
      }
      catch (OperationCanceledException ex)
      {
        logger.LogWarning(ex, "Pipeline execution was canceled: {PipelineId}", pipeline.Id);
        result.Success = false;
        result.ErrorMessage = "Pipeline execution was canceled";
        executionLog.Status = EtlExecutionStatus.Canceled;
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Error executing pipeline: {PipelineId}", pipeline.Id);
        result.Success = false;
        result.ErrorMessage = ex.Message;
        executionLog.Status = EtlExecutionStatus.Failed;
        executionLog.ErrorMessage = ex.ToString();
      }
      finally
      {
        stopwatch.Stop();
        result.ExecutionTimeMs = stopwatch.ElapsedMilliseconds;

        executionLog.EndTime = DateTime.UtcNow;
        executionLog.DurationMs = stopwatch.ElapsedMilliseconds;
        executionLog.ExtractedRowCount = result.ExtractedRowCount;
        executionLog.TransformedRowCount = result.TransformedRowCount;
        executionLog.LoadedRowCount = result.LoadedRowCount;

        await executionLogRepository.UpdateAsync(executionLog, cancellationToken);

        logger.LogInformation(
          "Pipeline execution completed in {ExecutionTime}ms with status {Status}",
          result.ExecutionTimeMs,
          executionLog.Status
        );
      }

      return result;
    }
  }
}
