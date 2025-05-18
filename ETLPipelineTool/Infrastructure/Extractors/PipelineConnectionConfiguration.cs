namespace ETLPipelineTool.Infrastructure.Extractors
{
    public class PipelineConnectionConfiguration
    {
        public string ConnectionString { get; set; } = default!;
        public string? Query { get; set; }
    }
}
