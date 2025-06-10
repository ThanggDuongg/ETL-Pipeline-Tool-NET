namespace ETLPipelineTool.Application.Features.EtlPipelines.Commands
{
  public class RunEtlPipelineCommandValidator : AbstractValidator<RunEtlPipelineCommand>
  {
    public RunEtlPipelineCommandValidator()
    {
      RuleFor(x => x.Id).NotEmpty();
    }
  }
}
