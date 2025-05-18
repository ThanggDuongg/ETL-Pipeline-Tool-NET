using ETLPipelineTool.Application.Dtos.V1.Responses.EtlPipelines;
using ETLPipelineTool.Application.Dtos.V1.Responses.FieldMappings;

namespace ETLPipelineTool.Application.Features.FieldMappings.Projections
{
    public static class FieldMappingProjection
    {
        public static Expression<Func<FieldMapping, FieldMappingDataDto>> AsFieldMappingDataDto()
        {
            return x => new FieldMappingDataDto(
                x.Id,
                new EtlPipelineDataDto(
                    x.EtlPipelineId,
                    x.EtlPipeline!.Name,
                    x.EtlPipeline.Description,
                    x.EtlPipeline.SourceType,
                    x.EtlPipeline.TargetType,
                    x.EtlPipeline.SourceConfigurationJson,
                    x.EtlPipeline.TargetConfigurationJson,
                    x.EtlPipeline.IsActive,
                    x.EtlPipeline.RowVersion
                ),
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
