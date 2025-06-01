using ETLPipelineTool.Application.Dtos.V1.Responses;
using ETLPipelineTool.Application.Dtos.V1.Responses.PipelineSchedules;
using ETLPipelineTool.Application.Features.Common.Queries;

namespace ETLPipelineTool.Application.Features.PipelineSchedules.Queries
{
  public class GetPipelineSchedulesQuery : GridQuery<GridResultDataDto<PipelineScheduleDataDto>>
  {
    public Guid? EtlPipelineId { get; set; }
    public bool? IsActive { get; set; }

    public GetPipelineSchedulesQuery(
      int take,
      int skip,
      bool preloadAllData,
      ICollection<SortField> sortFields,
      Guid? etlPipelineId = null,
      bool? isActive = null
    )
      : base(take, skip, preloadAllData, sortFields)
    {
      EtlPipelineId = etlPipelineId;
      IsActive = isActive;
    }
  }
}
