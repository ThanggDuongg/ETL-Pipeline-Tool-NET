namespace ETLPipelineTool.Application.Features.EtlPipelines.Commands
{
    public class CreateEtlPipelineCommandValidator : AbstractValidator<CreateEtlPipelineCommand>
    {
        public CreateEtlPipelineCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.SourceConfigurationJson).NotEmpty();
            RuleFor(x => x.TargetConfigurationJson).NotEmpty();
            RuleFor(x => x.SourceType).IsInEnum();
            RuleFor(x => x.TargetType).IsInEnum();
        }
    }
}
