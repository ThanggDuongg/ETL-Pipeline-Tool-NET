namespace ETLPipelineTool.Application.Features.FieldMappings.Commands
{
    public class CreateFieldMappingCommandValidator : AbstractValidator<CreateFieldMappingCommand>
    {
        public CreateFieldMappingCommandValidator()
        {
            RuleFor(x => x.EtlPipelineId).NotEmpty();
            RuleFor(x => x.Order).GreaterThanOrEqualTo(0);
            RuleFor(x => x.SourceField).NotEmpty();
            RuleFor(x => x.TargetField).NotEmpty();
        }
    }
}
