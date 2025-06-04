using ETLPipelineTool.Application.Dtos.V1.Responses.TransformRules;
using ETLPipelineTool.Application.Features.TransformRules.Projections;

namespace ETLPipelineTool.Application.Features.TransformRules.Queries;

public class GetTransformRulesQueryHandler(ITransformRuleRepository repository)
  : GridQueryHandler<GetTransformRulesQuery, TransformRule, TransformRuleDataDto>
{
  protected override IQueryable<TransformRule> GetBaseQuery(GetTransformRulesQuery request)
  {
    return repository.Get();
  }

  protected override IQueryable<TransformRule> ApplyFiltering(
    IQueryable<TransformRule> query,
    GetTransformRulesQuery request
  )
  {
    if (request.FieldMappingId.HasValue)
    {
      query = query.Where(x => x.FieldMappingId == request.FieldMappingId.Value);
    }

    return query;
  }

  protected override Expression<Func<TransformRule, TransformRuleDataDto>> BuildFullProjection()
  {
    return TransformRuleProjection.AsTransformRuleDataDto();
  }
}
