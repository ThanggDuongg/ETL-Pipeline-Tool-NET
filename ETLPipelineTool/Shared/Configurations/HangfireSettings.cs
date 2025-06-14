namespace ETLPipelineTool.Shared.Configurations
{
  public class HangfireSettings
  {
    public int WorkerCount { get; set; }
    public string[] Queues { get; set; } = [];
    public int RetryAttempts { get; set; } = 3;
    public int RetryDelayInSeconds { get; set; } = 60;
  }
}
