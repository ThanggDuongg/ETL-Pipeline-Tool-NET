namespace ETLPipelineTool.Application.Dtos.V1.Requests.AuditLogs;

public record GridAuditLogsFilterDto(
  GridDataSourceDto GridDataSourceDto,
  string? TableName = null,
  string? ActionType = null,
  DateTime? CreatedOnFrom = null,
  DateTime? CreatedOnTo = null
);
