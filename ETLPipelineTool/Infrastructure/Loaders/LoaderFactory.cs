using ETLPipelineTool.Infrastructure.Loaders.Interfaces;

namespace ETLPipelineTool.Infrastructure.Loaders
{
  public class LoaderFactory(IServiceProvider serviceProvider) : ILoaderFactory
  {
    public ILoader Create(PipelineTargetType type) =>
      type switch
      {
        PipelineTargetType.MssqlDatabase => serviceProvider.GetRequiredService<MssqlLoader>(),
        _ => throw new NotSupportedException($"Unsupported target type {type}"),
      };
  }
}
