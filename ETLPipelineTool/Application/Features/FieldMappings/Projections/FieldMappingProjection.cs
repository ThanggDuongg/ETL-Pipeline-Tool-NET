using ETLPipelineTool.Application.Dtos.V1.Responses.FieldMappings;
using ETLPipelineTool.Application.Dtos.V1.Responses.TransformRules;

namespace ETLPipelineTool.Application.Features.FieldMappings.Projections
{
  public static class FieldMappingProjection
  {
    public static Expression<Func<FieldMapping, FieldMappingDataDto>> AsFieldMappingDataDto()
    {
      return x => new FieldMappingDataDto(
        x.Id,
        x.EtlPipelineId,
        x.Order,
        x.SourceFields.OrderBy(sf => sf.Order)
          .Select(sf => new FieldMappingSourceDataDto(
            sf.Id,
            sf.Order,
            sf.SourceField,
            sf.RowVersion
          ))
          .ToList(),
        x.TargetField,
        x.TransformRules.OrderBy(tr => tr.Sequence)
          .Select(tr => new TransformRuleDataDto(
            tr.Id,
            tr.FieldMappingId,
            tr.Sequence,
            tr.RuleType,
            tr.RuleConfigurationJson,
            tr.RowVersion
          ))
          .ToList(),
        x.RowVersion
      );
    }
  }
}
