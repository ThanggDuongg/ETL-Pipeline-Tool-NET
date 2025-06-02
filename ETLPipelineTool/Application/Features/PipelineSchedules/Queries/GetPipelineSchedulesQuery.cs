using ETLPipelineTool.Application.Dtos.V1.Responses;
using ETLPipelineTool.Application.Dtos.V1.Responses.PipelineSchedules;

namespace ETLPipelineTool.Application.Features.PipelineSchedules.Queries
{
  public class GetPipelineSchedulesQuery(
    int take,
    int skip,
    bool preloadAllData,
    ICollection<SortField> sortFields,
    Guid? etlPipelineId = null,
    bool? isActive = null
  ) : GridQuery<GridResultDataDto<PipelineScheduleDataDto>>(take, skip, preloadAllData, sortFields)
  {
    public Guid? EtlPipelineId { get; } = etlPipelineId;
    public bool? IsActive { get; } = isActive;
  }
}
