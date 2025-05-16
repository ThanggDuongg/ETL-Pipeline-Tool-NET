namespace ETLPipelineTool.Application.Features.FieldMappings.Commands
{
    public class UpdateFieldMappingCommandValidator : AbstractValidator<UpdateFieldMappingCommand>
    {
        public UpdateFieldMappingCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Order).GreaterThanOrEqualTo(0);
            RuleFor(x => x.SourceFields).NotEmpty();
            RuleFor(x => x.TargetField).NotEmpty();
            RuleFor(x => x.TransformRuleType).NotEmpty();
            RuleFor(x => x.RowVersion).NotEmpty();
        }
    }
}
