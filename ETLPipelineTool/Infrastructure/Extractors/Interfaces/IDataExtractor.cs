namespace ETLPipelineTool.Infrastructure.Extractors.Interfaces
{
    public interface IDataExtractor
    {
        IAsyncEnumerable<ExtractedRow> ExtractAsync(
            PipelineConnectionConfiguration config,
            CancellationToken cancellationToken = default
        );
    }
}
