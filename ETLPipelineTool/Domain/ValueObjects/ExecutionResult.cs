namespace ETLPipelineTool.Domain.ValueObjects
{
  public class ExecutionResult
  {
    public Guid PipelineId { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public long ExecutionTimeMs { get; set; }
    public int ExtractedRowCount { get; set; }
    public int TransformedRowCount { get; set; }
    public int LoadedRowCount { get; set; }

    public ExecutionResult()
    {
      Success = false;
      ExtractedRowCount = 0;
      TransformedRowCount = 0;
      LoadedRowCount = 0;
    }

    public ExecutionResult(Guid pipelineId)
      : this()
    {
      PipelineId = pipelineId;
    }
  }
}
