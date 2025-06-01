namespace ETLPipelineTool.Application.Features.TransformRules.Commands;

public class UpdateTransformRuleCommandValidator : AbstractValidator<UpdateTransformRuleCommand>
{
  public UpdateTransformRuleCommandValidator()
  {
    RuleFor(x => x.Id).NotEmpty();
    RuleFor(x => x.RuleConfigurationJson).NotEmpty();
    RuleFor(x => x.RowVersion).NotNull();
  }
}
