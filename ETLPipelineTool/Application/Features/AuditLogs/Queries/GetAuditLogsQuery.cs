using ETLPipelineTool.Application.Dtos.V1.Responses;
using ETLPipelineTool.Application.Dtos.V1.Responses.AuditLogs;

namespace ETLPipelineTool.Application.Features.AuditLogs.Queries;

public class GetAuditLogsQuery(
  int take,
  int skip,
  bool preloadAllData,
  ICollection<SortField> sortFields,
  string? tableName = null,
  string? actionType = null,
  DateTime? createdOnFrom = null,
  DateTime? createdOnTo = null
) : GridQuery<GridResultDataDto<AuditLogDataDto>>(take, skip, preloadAllData, sortFields)
{
  public string? TableName { get; } = tableName;
  public string? ActionType { get; } = actionType;
  public DateTime? CreatedOnFrom { get; } = createdOnFrom;
  public DateTime? CreatedOnTo { get; } = createdOnTo;
}
