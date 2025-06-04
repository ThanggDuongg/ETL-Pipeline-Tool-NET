namespace ETLPipelineTool.Infrastructure.Configurations
{
  public class SqlSourceConfig
  {
    public string ConnectionString { get; init; } = default!;
    public int? CommandTimeout { get; init; } = 30;
    public int? BatchSize { get; init; } = 1000;
    public bool UseColumnMetadata { get; init; } = true;
    public string? Query { get; init; } = null;
  }
}
