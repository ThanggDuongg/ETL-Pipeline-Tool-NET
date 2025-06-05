using ETLPipelineTool.Infrastructure.Extractors;
using ETLPipelineTool.Infrastructure.Extractors.Decorators;
using ETLPipelineTool.Infrastructure.Extractors.Interfaces;
using ETLPipelineTool.Infrastructure.Transformers;
using ETLPipelineTool.Infrastructure.Transformers.Interfaces;

namespace ETLPipelineTool.Api.Extensions
{
  public static class InfrastructureServiceExtension
  {
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
      // Entity-specific repositories
      services.AddScoped<IEtlPipelineRepository, EtlPipelineRepository>();
      services.AddScoped<IFieldMappingRepository, FieldMappingRepository>();
      services.AddScoped<IEtlExecutionLogRepository, EtlExecutionLogRepository>();
      services.AddScoped<ITableSchemaRepository, TableSchemaRepository>();
      services.AddScoped<IPipelineScheduleRepository, PipelineScheduleRepository>();
      services.AddScoped<ITransformRuleRepository, TransformRuleRepository>();
      services.AddScoped<IAuditLogRepository, AuditLogRepository>();

      // Generic repository
      services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

      // Extractors
      services.AddScoped<IExtractor, MssqlExtractor>();
      services.AddScoped<IExtractorFactory, ExtractorFactory>();
      services.Decorate<IExtractor, LoggingExtractorDecorator>();
      services.AddScoped<ISchemaExtractor, MssqlSchemaExtractor>();

      // Transformers
      services.AddTransient<IdentityTransformRule>();
      services.AddTransient<ConcatTransformRule>();
      services.AddTransient<IfNullTransformRule>();
      services.AddTransient<RegexTransformRule>();
      services.AddScoped<ITransformRuleFactory, TransformRuleFactory>();
      services.AddScoped<ITransformer, Transformer>();
      services.Decorate<ITransformer, LoggingTransformerDecorator>();

      // Loaders
      services.AddScoped<MsSqlLoader>();
      services.AddScoped<ILoaderFactory, LoaderFactory>();
      services.AddTransient<ILoader, MsSqlLoader>();
      services.Decorate<ILoader, LoggingLoaderDecorator>();

      return services;
    }
  }
}
