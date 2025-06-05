namespace ETLPipelineTool.Infrastructure.Configurations
{
  public class SqlTargetConfig
  {
    public string ConnectionString { get; set; } = default!;
    public bool CreateDatabaseIfNotExists { get; set; } = true;
    public bool CreateTablesIfNotExist { get; set; } = true;
    public string SchemaName { get; set; } = "dbo";

    // Optional table name mapping (source table name -> target table name)
    public Dictionary<string, string>? TableMapping { get; set; }
    public bool UseBulkCopy { get; set; } = true;
    public bool UseBatchInsert { get; set; } = false;
    public int BatchSize { get; set; } = 1000;
    public int CommandTimeout { get; set; } = 60;
  }
}
