namespace ETLPipelineTool.Infrastructure.Loaders.Interfaces
{
  public interface ILoaderFactory
  {
    ILoader Create(PipelineTargetType type);
  }
}
