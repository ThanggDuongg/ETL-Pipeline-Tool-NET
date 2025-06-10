using ETLPipelineTool.Domain.ValueObjects;

namespace ETLPipelineTool.Application.Services.Interfaces
{
  public interface IPipelineOrchestrator
  {
    Task<ExecutionResult> ExecutePipelineAsync(
      EtlPipeline pipeline,
      CancellationToken cancellationToken = default
    );
  }
}
