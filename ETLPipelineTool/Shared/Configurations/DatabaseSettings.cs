namespace ETLPipelineTool.Shared.Configurations
{
  public class DatabaseSettings
  {
    public bool EnableSensitiveDataLogging { get; set; } = false;
    public int CommandTimeout { get; set; } = 30;
  }
}
