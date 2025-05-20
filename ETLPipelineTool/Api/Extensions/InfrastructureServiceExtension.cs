using ETLPipelineTool.Infrastructure.Extractors;
using ETLPipelineTool.Infrastructure.Extractors.Decorators;
using ETLPipelineTool.Infrastructure.Extractors.Interfaces;

namespace ETLPipelineTool.Api.Extensions
{
    public static class InfrastructureServiceExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IEtlPipelineRepository, EtlPipelineRepository>();
            services.AddScoped<IFieldMappingRepository, FieldMappingRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            // Extractors
            services.AddScoped<IExtractor, MssqlExtractor>();
            services.AddScoped<IExtractorFactory, ExtractorFactory>();
            services.Decorate<IExtractor, LoggingExtractorDecorator>();

            return services;
        }
    }
}
