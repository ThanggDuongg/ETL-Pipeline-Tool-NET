namespace ETLPipelineTool.Application.Features.EtlPipelines.Commands
{
  public class DeleteEtlPipelineCommandValidator : AbstractValidator<DeleteEtlPipelineCommand>
  {
    public DeleteEtlPipelineCommandValidator()
    {
      RuleFor(x => x.Id).NotEmpty();
    }
  }
}
