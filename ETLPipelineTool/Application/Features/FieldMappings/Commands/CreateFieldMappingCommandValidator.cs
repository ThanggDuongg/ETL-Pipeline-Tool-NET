namespace ETLPipelineTool.Application.Features.FieldMappings.Commands
{
    public class CreateFieldMappingCommandValidator : AbstractValidator<CreateFieldMappingCommand>
    {
        public CreateFieldMappingCommandValidator()
        {
            RuleFor(x => x.EtlPipelineId).NotEmpty();
            RuleFor(x => x.Order).GreaterThanOrEqualTo(0);
            RuleFor(x => x.SourceFields).NotEmpty();
            RuleFor(x => x.TargetField).NotEmpty();
            RuleFor(x => x.TransformRuleType).NotEmpty();
        }
    }
}
