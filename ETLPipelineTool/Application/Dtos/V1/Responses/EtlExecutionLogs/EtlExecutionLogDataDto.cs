using ETLPipelineTool.Application.Dtos.V1.Responses.EtlPipelines;

namespace ETLPipelineTool.Application.Dtos.V1.Responses.EtlExecutionLogs;

public record EtlExecutionLogDataDto
{
  public Guid Id { get; init; }
  public EtlPipelineDataDto EtlPipeline { get; init; } = default!;
  public DateTime StartedAt { get; init; }
  public DateTime? FinishedAt { get; init; }
  public EtlExecutionStatus Status { get; init; }
  public int RecordsProcessed { get; init; }
  public int RecordsSucceeded { get; init; }
  public int RecordsFailed { get; init; }
  public double ProcessingTimeMs { get; init; }
  public string? ErrorMessage { get; init; }
  public string? ErrorDetails { get; init; }

  public EtlExecutionLogDataDto() { }

  public EtlExecutionLogDataDto(
    Guid id,
    EtlPipelineDataDto etlPipeline,
    DateTime startedAt,
    DateTime? finishedAt,
    EtlExecutionStatus status,
    int recordsProcessed,
    int recordsSucceeded,
    int recordsFailed,
    double processingTimeMs,
    string? errorMessage,
    string? errorDetails
  )
  {
    Id = id;
    EtlPipeline = etlPipeline;
    StartedAt = startedAt;
    FinishedAt = finishedAt;
    Status = status;
    RecordsProcessed = recordsProcessed;
    RecordsSucceeded = recordsSucceeded;
    RecordsFailed = recordsFailed;
    ProcessingTimeMs = processingTimeMs;
    ErrorMessage = errorMessage;
    ErrorDetails = errorDetails;
  }
}
