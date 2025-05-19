namespace ETLPipelineTool.Domain.ValueObjects
{
    public record TableData(string TableName, IAsyncEnumerable<IDictionary<string, object>> Rows);
}
