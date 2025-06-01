using FluentValidation;

namespace ETLPipelineTool.Application.Features.TransformRules.Queries;

public class GetTransformRulesQueryValidator : AbstractValidator<GetTransformRulesQuery>
{
  public GetTransformRulesQueryValidator() { }
}
