using ETLPipelineTool.Application.Services.Interfaces;

namespace ETLPipelineTool.Application.Features.EtlPipelines.Commands
{
  public class RunEtlPipelineCommandHandler(
    IBackgroundJobService backgroundJobService,
    ILogger<RunEtlPipelineCommandHandler> logger
  ) : IRequestHandler<RunEtlPipelineCommand, Unit>
  {
    public Task<Unit> Handle(RunEtlPipelineCommand command, CancellationToken cancellationToken)
    {
      logger.LogInformation("Enqueueing ETL pipeline {PipelineId}", command.Id);

      var jobId = backgroundJobService.EnqueueEtlPipeline(command.Id);

      logger.LogInformation(
        "ETL pipeline {PipelineId} enqueued successfully with job ID {JobId}",
        command.Id,
        jobId
      );

      return Task.FromResult(Unit.Value);
    }
  }
}
