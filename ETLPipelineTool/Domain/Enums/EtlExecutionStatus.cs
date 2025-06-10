namespace ETLPipelineTool.Domain.Enums
{
  public enum EtlExecutionStatus
  {
    Pending = 0,
    Running,
    Completed,
    Failed,
    Canceled,
  }
}
