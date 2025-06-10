namespace ETLPipelineTool.Domain.Entities
{
  public class EtlExecutionLog : BaseEntity
  {
    public Guid EtlPipelineId { get; set; }
    public EtlPipeline? EtlPipeline { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public long DurationMs { get; set; }
    public EtlExecutionStatus Status { get; set; }
    public string? ErrorMessage { get; set; }
    public int ExtractedRowCount { get; set; }
    public int TransformedRowCount { get; set; }
    public int LoadedRowCount { get; set; }
  }
}
