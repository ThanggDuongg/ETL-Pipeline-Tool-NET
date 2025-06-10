namespace ETLPipelineTool.Application.Features.EtlPipelines.Commands
{
  public class RunEtlPipelineCommand(Guid id) : IRequest<Unit>
  {
    public Guid Id { get; } = id;
  }
}
