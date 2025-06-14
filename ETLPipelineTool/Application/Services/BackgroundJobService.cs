using ETLPipelineTool.Application.Services.Interfaces;
using Hangfire;

namespace ETLPipelineTool.Application.Services
{
  public class BackgroundJobService(
    IPipelineOrchestrator orchestrator,
    IEtlPipelineRepository pipelineRepository,
    ILogger<BackgroundJobService> logger
  ) : IBackgroundJobService
  {
    public string EnqueueEtlPipeline(Guid pipelineId)
    {
      logger.LogInformation("Enqueueing ETL pipeline {PipelineId}", pipelineId);
      return BackgroundJob.Enqueue(() => ExecutePipelineAsync(pipelineId, CancellationToken.None));
    }

    public string ScheduleEtlPipeline(Guid pipelineId, string cronExpression)
    {
      logger.LogInformation(
        "Scheduling ETL pipeline {PipelineId} with cron expression {CronExpression}",
        pipelineId,
        cronExpression
      );

      var jobId = pipelineId.ToString();
      RecurringJob.AddOrUpdate(
        jobId,
        () => ExecutePipelineAsync(pipelineId, CancellationToken.None),
        cronExpression
      );
      return jobId;
    }

    public void CancelJob(string jobId)
    {
      logger.LogInformation("Cancelling job {JobId}", jobId);
      BackgroundJob.Delete(jobId);
    }

    public void DeleteRecurringJob(string jobId)
    {
      logger.LogInformation("Deleting recurring job {JobId}", jobId);
      RecurringJob.RemoveIfExists(jobId);
    }

    public async Task ExecutePipelineAsync(Guid pipelineId, CancellationToken cancellationToken)
    {
      try
      {
        logger.LogInformation("Starting execution of ETL pipeline {PipelineId}", pipelineId);

        var pipeline = await pipelineRepository.GetByIdAsync(
          id: pipelineId,
          predicate: null,
          include: e =>
            e.Include(x => x.TableSchemas)
              .ThenInclude(x => x.Columns)
              .Include(x => x.FieldMappings)
              .ThenInclude(x => x.SourceFields)
              .Include(x => x.FieldMappings)
              .ThenInclude(x => x.TransformRules),
          isTracking: false,
          asSplitQuery: true,
          cancellationToken: cancellationToken
        );

        var result = await orchestrator.ExecutePipelineAsync(pipeline, cancellationToken);

        logger.LogInformation(
          "Completed execution of ETL pipeline {PipelineId} with success={Success}",
          pipelineId,
          result.Success
        );
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Error executing ETL pipeline {PipelineId}", pipelineId);
        throw;
      }
    }
  }
}
