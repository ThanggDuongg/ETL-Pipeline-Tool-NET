namespace ETLPipelineTool.Infrastructure.Extractors
{
    public class ExtractedRow
    {
        public string TableName { get; set; } = default!;
        public Dictionary<string, object?> Columns { get; set; } = [];
    }
}
