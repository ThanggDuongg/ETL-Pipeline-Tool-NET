namespace ETLPipelineTool.Application.Services.Interfaces
{
  public interface IBackgroundJobService
  {
    string EnqueueEtlPipeline(Guid pipelineId);

    string ScheduleEtlPipeline(Guid pipelineId, string cronExpression);

    void CancelJob(string jobId);

    void DeleteRecurringJob(string jobId);

    Task ExecutePipelineAsync(Guid pipelineId, CancellationToken cancellationToken);
  }
}
