namespace ETLPipelineTool.Application.Features.PipelineSchedules.Commands;

public class CreatePipelineScheduleCommand(
  Guid EtlPipelineId,
  string CronExpression,
  bool IsActive,
  DateTime? StartDate,
  DateTime? EndDate
) : IRequest<Guid>
{
  public Guid EtlPipelineId { get; } = EtlPipelineId;
  public string CronExpression { get; } = CronExpression;
  public bool IsActive { get; } = IsActive;
  public DateTime? StartDate { get; } = StartDate;
  public DateTime? EndDate { get; } = EndDate;
}
