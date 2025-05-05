using ETLPipelineTool.Application.Dtos.V1.Responses;

namespace ETLPipelineTool.Application.Features.EtlPipelines.Projections
{
    public static class EtlPipelineProjection
    {
        public static Expression<Func<EtlPipeline, EtlPipelineDataDto>> AsEtlPipelineDataDto()
        {
            return x => new EtlPipelineDataDto(
                x.Id,
                x.Name,
                x.Description,
                x.SourceType,
                x.TargetType,
                x.SourceConfigurationJson,
                x.TargetConfigurationJson,
                x.IsActive,
                x.RowVersion
            );
        }
    }
}
