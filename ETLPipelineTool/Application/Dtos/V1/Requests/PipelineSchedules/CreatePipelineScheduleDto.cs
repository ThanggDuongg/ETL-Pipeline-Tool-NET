namespace ETLPipelineTool.Application.Dtos.V1.Requests.PipelineSchedules;

public record CreatePipelineScheduleDto(
  Guid EtlPipelineId,
  string CronExpression,
  bool IsActive,
  DateTime? StartDate,
  DateTime? EndDate
);
