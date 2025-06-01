using ETLPipelineTool.Application.Dtos.V1.Responses.EtlExecutionLogs;
using ETLPipelineTool.Application.Dtos.V1.Responses.EtlPipelines;

namespace ETLPipelineTool.Application.Features.EtlExecutionLogs.Projections
{
  public static class EtlExecutionLogProjection
  {
    public static Expression<
      Func<EtlExecutionLog, EtlExecutionLogDataDto>
    > AsEtlExecutionLogDataDto()
    {
      return x => new EtlExecutionLogDataDto
      {
        Id = x.Id,
        EtlPipeline = new EtlPipelineDataDto
        {
          Id = x.EtlPipelineId,
          Name = x.EtlPipeline!.Name,
          Description = x.EtlPipeline.Description,
          SourceType = x.EtlPipeline.SourceType,
          TargetType = x.EtlPipeline.TargetType,
          SourceConfigurationJson = x.EtlPipeline.SourceConfigurationJson,
          TargetConfigurationJson = x.EtlPipeline.TargetConfigurationJson,
          IsActive = x.EtlPipeline.IsActive,
          RowVersion = x.EtlPipeline.RowVersion,
        },
        StartedAt = x.StartedAt,
        FinishedAt = x.FinishedAt,
        Status = x.Status,
        RecordsProcessed = x.RecordsProcessed,
        RecordsSucceeded = x.RecordsSucceeded,
        RecordsFailed = x.RecordsFailed,
        ProcessingTimeMs = x.ProcessingTimeMs,
        ErrorMessage = x.ErrorMessage,
        ErrorDetails = x.ErrorDetails,
      };
    }
  }
}
