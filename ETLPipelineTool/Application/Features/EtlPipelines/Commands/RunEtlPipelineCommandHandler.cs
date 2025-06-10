using ETLPipelineTool.Application.Services.Interfaces;

namespace ETLPipelineTool.Application.Features.EtlPipelines.Commands
{
  public class RunEtlPipelineCommandHandler(
    IEtlPipelineRepository etlPipelineRepository,
    IPipelineOrchestrator pipelineOrchestrator,
    ILogger<RunEtlPipelineCommandHandler> logger
  ) : IRequestHandler<RunEtlPipelineCommand, Unit>
  {
    public async Task<Unit> Handle(
      RunEtlPipelineCommand command,
      CancellationToken cancellationToken
    )
    {
      logger.LogInformation("Starting execution of ETL pipeline {PipelineId}", command.Id);

      var etlPipeline = await etlPipelineRepository.GetByIdAsync(command.Id, cancellationToken);

      // Start pipeline execution in background using "fire and forget" pattern
      // This allows the API to respond immediately while the ETL process continues running
      // The client doesn't need to maintain an open connection for potentially long-running ETL operations
      // Using Task.Run with discarded result (_=) explicitly indicates we're not awaiting the task completion
      _ = Task.Run(
        async () =>
        {
          try
          {
            // Execute pipeline - the orchestrator will handle the execution log
            // Using CancellationToken.None because this task runs independently of the original request
            var result = await pipelineOrchestrator.ExecutePipelineAsync(
              etlPipeline,
              CancellationToken.None
            );

            logger.LogInformation(
              "Completed execution of ETL pipeline {PipelineId} with success={Success}",
              command.Id,
              result.Success
            );
          }
          catch (Exception ex)
          {
            logger.LogError(ex, "Error executing ETL pipeline {PipelineId}", command.Id);
          }
        },
        CancellationToken.None
      );

      return Unit.Value;
    }
  }
}
