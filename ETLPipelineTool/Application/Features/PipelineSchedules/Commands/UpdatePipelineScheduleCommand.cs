namespace ETLPipelineTool.Application.Features.PipelineSchedules.Commands;

public class UpdatePipelineScheduleCommand(
  Guid Id,
  string CronExpression,
  bool IsActive,
  DateTime? StartDate,
  DateTime? EndDate,
  byte[] RowVersion
) : IRequest<Unit>
{
  public Guid Id { get; } = Id;
  public string CronExpression { get; } = CronExpression;
  public bool IsActive { get; } = IsActive;
  public DateTime? StartDate { get; } = StartDate;
  public DateTime? EndDate { get; } = EndDate;
  public byte[] RowVersion { get; } = RowVersion;
}
