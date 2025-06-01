namespace ETLPipelineTool.Application.Dtos.V1.Requests.EtlExecutionLogs;

public record GridEtlExecutionLogsFilterDto
{
  public GridDataSourceDto GridDataSourceDto { get; init; } = new();
  public Guid? EtlPipelineId { get; init; }
  public EtlExecutionStatus? Status { get; init; }
  public DateTime? StartedAtFrom { get; init; }
  public DateTime? StartedAtTo { get; init; }
}
