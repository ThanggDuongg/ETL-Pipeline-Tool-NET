using ETLPipelineTool.Application.Dtos.V1.Requests.EtlExecutionLogs;
using ETLPipelineTool.Application.Dtos.V1.Responses.EtlExecutionLogs;
using ETLPipelineTool.Application.Dtos.V1.Responses.EtlPipelines;
using ETLPipelineTool.Application.Features.EtlExecutionLogs.Queries;

namespace ETLPipelineTool.Application.Features.EtlExecutionLogs.Mappings;

public static class EtlExecutionLogMapper
{
  public static GetEtlExecutionLogsQuery ToGetEtlExecutionLogsQuery(
    GridEtlExecutionLogsFilterDto dto
  )
  {
    return new GetEtlExecutionLogsQuery(
      dto.GridDataSourceDto.Take,
      dto.GridDataSourceDto.Skip,
      dto.GridDataSourceDto.PreloadAllData,
      dto.GridDataSourceDto.SortFields,
      dto.EtlPipelineId,
      dto.Status,
      dto.StartedAtFrom,
      dto.StartedAtTo
    );
  }

  public static EtlExecutionLogDataDto ToEtlExecutionLogDataDto(EtlExecutionLog entity)
  {
    return new EtlExecutionLogDataDto
    {
      Id = entity.Id,
      EtlPipeline = new EtlPipelineDataDto
      {
        Id = entity.EtlPipelineId,
        Name = entity.EtlPipeline?.Name ?? string.Empty,
        Description = entity.EtlPipeline!.Description,
        SourceType = entity.EtlPipeline.SourceType,
        TargetType = entity.EtlPipeline.TargetType,
        SourceConfigurationJson = entity.EtlPipeline.SourceConfigurationJson,
        TargetConfigurationJson = entity.EtlPipeline.TargetConfigurationJson,
        IsActive = entity.EtlPipeline.IsActive,
        RowVersion = entity.EtlPipeline.RowVersion,
      },
      StartedAt = entity.StartedAt,
      FinishedAt = entity.FinishedAt,
      Status = entity.Status,
      RecordsProcessed = entity.RecordsProcessed,
      RecordsSucceeded = entity.RecordsSucceeded,
      RecordsFailed = entity.RecordsFailed,
      ProcessingTimeMs = entity.ProcessingTimeMs,
      ErrorMessage = entity.ErrorMessage,
      ErrorDetails = entity.ErrorDetails,
    };
  }
}
