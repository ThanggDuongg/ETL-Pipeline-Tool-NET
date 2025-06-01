namespace ETLPipelineTool.Application.Dtos.V1.Requests.PipelineSchedules;

public record GridPipelineSchedulesFilterDto(
  GridDataSourceDto GridDataSourceDto,
  Guid? EtlPipelineId = null,
  bool? IsActive = null
);
