using Hangfire;
using Hangfire.SqlServer;

namespace ETLPipelineTool.Api.Extensions
{
  public static class HangfireServiceExtension
  {
    public static IServiceCollection AddHangfireServices(
      this IServiceCollection services,
      IConfiguration configuration
    )
    {
      services.AddHangfire(
        (sp, config) =>
          config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(
              configuration.GetConnectionString("DefaultConnection"),
              new SqlServerStorageOptions
              {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.FromSeconds(15),
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true,
              }
            )
      );

      // Configure Hangfire server
      var hangfireSettings = configuration.GetSection("Hangfire").Get<HangfireSettings>();
      services.AddHangfireServer(options =>
      {
        options.WorkerCount = hangfireSettings?.WorkerCount ?? Environment.ProcessorCount * 2;
        options.Queues = hangfireSettings?.Queues ?? ["etl", "default"];
      });

      return services;
    }
  }
}
