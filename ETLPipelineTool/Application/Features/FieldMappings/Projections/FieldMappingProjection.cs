using ETLPipelineTool.Application.Dtos.V1.Responses.FieldMappings;

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
                x.SourceFields,
                x.TargetField,
                x.TransformRuleType,
                x.TransformConfig,
                x.RowVersion
            );
        }
    }
}
