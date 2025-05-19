namespace ETLPipelineTool.Infrastructure.Loaders.Interfaces
{
    public interface ILoader
    {
        Task LoadAsync(
            EtlPipeline etlPipeline,
            IEnumerable<IDictionary<string, object>> transformedData,
            CancellationToken cancellationToken = default
        );
    }
}
