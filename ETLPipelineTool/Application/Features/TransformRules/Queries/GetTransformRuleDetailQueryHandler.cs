using ETLPipelineTool.Application.Dtos.V1.Responses.TransformRules;
using ETLPipelineTool.Application.Features.TransformRules.Projections;

namespace ETLPipelineTool.Application.Features.TransformRules.Queries;

public class GetTransformRuleDetailQueryHandler(ITransformRuleRepository repository)
  : IRequestHandler<GetTransformRuleDetailQuery, TransformRuleDataDto>
{
  public async Task<TransformRuleDataDto> Handle(
    GetTransformRuleDetailQuery request,
    CancellationToken cancellationToken
  )
  {
    return await repository.GetByIdAsync(
      request.Id,
      null,
      TransformRuleProjection.AsTransformRuleDataDto(),
      cancellationToken
    );
  }
}
