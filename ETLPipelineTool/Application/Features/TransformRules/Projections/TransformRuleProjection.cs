using ETLPipelineTool.Application.Dtos.V1.Responses.TransformRules;

namespace ETLPipelineTool.Application.Features.TransformRules.Projections
{
  public static class TransformRuleProjection
  {
    public static Expression<Func<TransformRule, TransformRuleDataDto>> AsTransformRuleDataDto()
    {
      return x => new TransformRuleDataDto(
        x.Id,
        x.FieldMappingId,
        x.Sequence,
        x.RuleType,
        x.RuleConfigurationJson,
        x.RowVersion
      );
    }
  }
}
