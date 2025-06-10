using ETLPipelineTool.Application.Dtos.V1.Responses.EtlExecutionLogs;

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
        EtlPipelineId = x.EtlPipelineId,
        EtlPipelineName = x.EtlPipeline!.Name,
        StartTime = x.StartTime,
        EndTime = x.EndTime,
        Status = x.Status,
        ExtractedRowCount = x.ExtractedRowCount,
        TransformedRowCount = x.TransformedRowCount,
        LoadedRowCount = x.LoadedRowCount,
        DurationMs = x.DurationMs,
        ErrorMessage = x.ErrorMessage,
      };
    }
  }
}
