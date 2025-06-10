namespace ETLPipelineTool.Application.Dtos.V1.Responses.EtlExecutionLogs;

public record EtlExecutionLogDataDto
{
  public Guid Id { get; init; }
  public Guid EtlPipelineId { get; init; }
  public DateTime StartTime { get; init; }
  public DateTime? EndTime { get; init; }
  public EtlExecutionStatus Status { get; init; }
  public int ExtractedRowCount { get; init; }
  public int TransformedRowCount { get; init; }
  public int LoadedRowCount { get; init; }
  public long DurationMs { get; init; }
  public string? ErrorMessage { get; init; }

  public EtlExecutionLogDataDto() { }

  public EtlExecutionLogDataDto(
    Guid id,
    Guid etlPipelineId,
    DateTime startTime,
    DateTime? endTime,
    EtlExecutionStatus status,
    int extractedRowCount,
    int transformedRowCount,
    int loadedRowCount,
    long durationMs,
    string? errorMessage
  )
  {
    Id = id;
    EtlPipelineId = etlPipelineId;
    StartTime = startTime;
    EndTime = endTime;
    Status = status;
    ExtractedRowCount = extractedRowCount;
    TransformedRowCount = transformedRowCount;
    LoadedRowCount = loadedRowCount;
    DurationMs = durationMs;
    ErrorMessage = errorMessage;
  }
}
