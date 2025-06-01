using ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings;
using ETLPipelineTool.Application.Dtos.V1.Requests.TransformRules;

namespace ETLPipelineTool.Application.Features.FieldMappings.Commands
{
  public class UpdateFieldMappingCommandValidator : AbstractValidator<UpdateFieldMappingCommand>
  {
    public UpdateFieldMappingCommandValidator()
    {
      RuleFor(x => x.FieldMapping.Id).NotEmpty();
      RuleFor(x => x.FieldMapping).NotNull();
      RuleFor(x => x.FieldMapping.Order).GreaterThanOrEqualTo(0);
      RuleFor(x => x.FieldMapping.TargetField).NotEmpty();
      RuleFor(x => x.FieldMapping.FieldMappingSources).NotNull();
      RuleForEach(x => x.FieldMapping.FieldMappingSources)
        .SetValidator(new UpdateFieldMappingSourceValidator());
      RuleForEach(x => x.FieldMapping.TransformRules)
        .SetValidator(new UpdateTransformRuleValidator());
      RuleFor(x => x.FieldMapping.RowVersion).NotNull();
    }
  }

  public class UpdateFieldMappingSourceValidator : AbstractValidator<UpdateFieldMappingSourceDto>
  {
    public UpdateFieldMappingSourceValidator()
    {
      RuleFor(x => x.Order).GreaterThanOrEqualTo(0);
      RuleFor(x => x.SourceField).NotEmpty();
    }
  }

  public class UpdateTransformRuleValidator : AbstractValidator<UpdateTransformRuleDto>
  {
    public UpdateTransformRuleValidator()
    {
      RuleFor(x => x.Sequence).GreaterThanOrEqualTo(0);
      RuleFor(x => x.RuleType).NotEmpty();
      RuleFor(x => x.RuleConfigurationJson).NotEmpty();
    }
  }
}
