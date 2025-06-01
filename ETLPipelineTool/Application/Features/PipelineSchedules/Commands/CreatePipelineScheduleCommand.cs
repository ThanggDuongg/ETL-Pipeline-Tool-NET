namespace ETLPipelineTool.Application.Features.PipelineSchedules.Commands;

public record CreatePipelineScheduleCommand(
  Guid EtlPipelineId,
  string CronExpression,
  bool IsActive,
  DateTime? StartDate,
  DateTime? EndDate
) : IRequest<Guid>;
