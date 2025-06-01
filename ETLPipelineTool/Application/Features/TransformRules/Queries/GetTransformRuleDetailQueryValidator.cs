using FluentValidation;

namespace ETLPipelineTool.Application.Features.TransformRules.Queries;

public class GetTransformRuleDetailQueryValidator : AbstractValidator<GetTransformRuleDetailQuery>
{
  public GetTransformRuleDetailQueryValidator()
  {
    RuleFor(x => x.Id).NotEmpty();
  }
}
