using ETLPipelineTool.Application.Dtos.V1.Responses;
using ETLPipelineTool.Application.Dtos.V1.Responses.EtlExecutionLogs;

namespace ETLPipelineTool.Application.Features.EtlExecutionLogs.Queries
{
  public class GetEtlExecutionLogsQuery : GridQuery<GridResultDataDto<EtlExecutionLogDataDto>>
  {
    public Guid? EtlPipelineId { get; }
    public EtlExecutionStatus? Status { get; }

    public GetEtlExecutionLogsQuery(
      int take,
      int skip,
      bool preloadAllData,
      ICollection<SortField> sortFields,
      Guid? etlPipelineId = null,
      EtlExecutionStatus? status = null
    )
      : base(take, skip, preloadAllData, sortFields)
    {
      EtlPipelineId = etlPipelineId;
      Status = status;
    }
  }
}
