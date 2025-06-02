using ETLPipelineTool.Application.Dtos.V1.Responses.TransformRules;

namespace ETLPipelineTool.Application.Features.TransformRules.Queries;

public class GetTransformRuleDetailQuery(Guid Id) : IRequest<TransformRuleDataDto>
{
  public Guid Id { get; } = Id;
}
