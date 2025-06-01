namespace ETLPipelineTool.Infrastructure.Transformers.Interfaces
{
  public interface ITransformer
  {
    Task<IEnumerable<IDictionary<string, object>>> TransformAsync(
      EtlPipeline etlPipeline,
      IEnumerable<IDictionary<string, object>> rawData,
      CancellationToken cancellationToken = default
    );
  }
}
