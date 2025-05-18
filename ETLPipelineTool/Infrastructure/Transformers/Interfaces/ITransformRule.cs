namespace ETLPipelineTool.Infrastructure.Transformers.Interfaces
{
    public interface ITransformRule
    {
        Task<object?> ApplyAsync(
            Dictionary<string, object?> columns,
            List<string> sourceFields,
            string? transformConfig,
            CancellationToken cancellationToken = default
        );
    }
}
