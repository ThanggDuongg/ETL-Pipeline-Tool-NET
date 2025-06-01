namespace ETLPipelineTool.Application.Features.PipelineSchedules.Commands;

public record UpdatePipelineScheduleCommand(
  Guid Id,
  Guid EtlPipelineId,
  string CronExpression,
  bool IsActive,
  DateTime? StartDate,
  DateTime? EndDate,
  byte[] RowVersion
) : IRequest<Unit>;
