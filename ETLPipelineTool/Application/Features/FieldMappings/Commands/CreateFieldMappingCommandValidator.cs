using ETLPipelineTool.Application.Dtos.V1.Requests.FieldMappings;

namespace ETLPipelineTool.Application.Features.FieldMappings.Commands
{
  public class CreateFieldMappingCommandValidator : AbstractValidator<CreateFieldMappingCommand>
  {
    public CreateFieldMappingCommandValidator()
    {
      RuleFor(x => x.FieldMapping).NotNull();
      RuleFor(x => x.FieldMapping.EtlPipelineId).NotEmpty();
      RuleFor(x => x.FieldMapping.Order).GreaterThanOrEqualTo(0);
      RuleFor(x => x.FieldMapping.TargetField).NotEmpty();
      RuleFor(x => x.FieldMapping.FieldMappingSources).NotNull();
      RuleForEach(x => x.FieldMapping.FieldMappingSources)
        .SetValidator(new CreateFieldMappingSourceValidator());
      RuleForEach(x => x.FieldMapping.TransformRules)
        .SetValidator(new CreateTransformRuleValidator());
    }
  }

  public class CreateFieldMappingSourceValidator : AbstractValidator<CreateFieldMappingSourceDto>
  {
    public CreateFieldMappingSourceValidator()
    {
      RuleFor(x => x.Order).GreaterThanOrEqualTo(0);
      RuleFor(x => x.SourceField).NotEmpty();
    }
  }

  public class CreateTransformRuleValidator : AbstractValidator<CreateTransformRuleDto>
  {
    public CreateTransformRuleValidator()
    {
      RuleFor(x => x.Sequence).GreaterThanOrEqualTo(0);
      RuleFor(x => x.RuleType).NotNull();
      RuleFor(x => x.RuleConfigurationJson).NotEmpty();
    }
  }
}
