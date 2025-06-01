namespace ETLPipelineTool.Application.Dtos.V1.Requests.PipelineSchedules;

public record UpdatePipelineScheduleDto(
  Guid Id,
  Guid EtlPipelineId,
  string CronExpression,
  bool IsActive,
  DateTime? StartDate,
  DateTime? EndDate,
  byte[] RowVersion
);
