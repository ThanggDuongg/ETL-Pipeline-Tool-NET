using ETLPipelineTool.Infrastructure.Extractors.Interfaces;

namespace ETLPipelineTool.Infrastructure.Extractors
{
    public class ExtractorFactory(IServiceProvider serviceProvider) : IExtractorFactory
    {
        public IExtractor Create(PipelineSourceType type) =>
            type switch
            {
                PipelineSourceType.MssqlDatabase =>
                    serviceProvider.GetRequiredService<MssqlExtractor>(),
                PipelineSourceType.CsvFile => serviceProvider.GetRequiredService<CsvExtractor>(),
                _ => throw new NotSupportedException($"Unsupported source {type}"),
            };
    }
}
