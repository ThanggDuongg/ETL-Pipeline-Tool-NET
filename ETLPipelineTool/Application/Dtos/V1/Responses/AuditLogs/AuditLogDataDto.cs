namespace ETLPipelineTool.Application.Dtos.V1.Responses.AuditLogs;

public record AuditLogDataDto
{
  public Guid Id { get; init; }
  public string TableName { get; init; } = default!;
  public string ActionType { get; init; } = default!;
  public string KeyValues { get; init; } = default!;
  public string? OldValues { get; init; }
  public string? NewValues { get; init; }
  public string CreatedBy { get; init; } = default!;
  public DateTime CreatedOn { get; init; }
}
