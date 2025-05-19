namespace ETLPipelineTool.Infrastructure.Extractors.Interfaces
{
    public interface IExtractorFactory
    {
        IExtractor Create(PipelineSourceType type);
    }
}
