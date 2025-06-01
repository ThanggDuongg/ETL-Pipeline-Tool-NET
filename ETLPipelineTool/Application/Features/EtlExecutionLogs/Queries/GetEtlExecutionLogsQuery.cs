using ETLPipelineTool.Application.Dtos.V1.Responses;
using ETLPipelineTool.Application.Dtos.V1.Responses.EtlExecutionLogs;

namespace ETLPipelineTool.Application.Features.EtlExecutionLogs.Queries;

public class GetEtlExecutionLogsQuery(
  int take,
  int skip,
  bool preloadAllData,
  ICollection<SortField> sortFields,
  Guid? etlPipelineId = null,
  EtlExecutionStatus? status = null,
  DateTime? startedAtFrom = null,
  DateTime? startedAtTo = null
) : GridQuery<GridResultDataDto<EtlExecutionLogDataDto>>(take, skip, preloadAllData, sortFields)
{
  public Guid? EtlPipelineId { get; } = etlPipelineId;
  public EtlExecutionStatus? Status { get; } = status;
  public DateTime? StartedAtFrom { get; } = startedAtFrom;
  public DateTime? StartedAtTo { get; } = startedAtTo;
}
