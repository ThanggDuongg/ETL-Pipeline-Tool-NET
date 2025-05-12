using ETLPipelineTool.Application.Dtos.V1.Responses;
using ETLPipelineTool.Application.Dtos.V1.Responses.EtlPipelines;

namespace ETLPipelineTool.Application.Features.EtlPipelines.Queries
{
    public class GetEtlPipelinesQuery(
        int take,
        int skip,
        bool preloadAllData,
        ICollection<SortField> sortFields,
        bool? isActive
    ) : GridQuery<GridResultDataDto<EtlPipelineDataDto>>(take, skip, preloadAllData, sortFields)
    {
        public bool? IsActive { get; set; } = isActive;
    }
}
