namespace ETLPipelineTool.Api.Extensions
{
    public static class DbContextExtension
    {
        public static IServiceCollection AddDbContextConfiguration(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            // Interceptors
            services.AddSingleton<AuditLogSaveChangesInterceptor>();
            services.AddSingleton<SlowQueryDetectionInterceptor>();

            // DbContexts
            services.AddDbContext<EtlContext>(
                (serviceProvider, options) =>
                {
                    var databaseSettings = serviceProvider
                        .GetRequiredService<IOptions<DatabaseSettings>>()
                        .Value;
                    var logger = serviceProvider.GetRequiredService<ILogger<EtlContext>>();
                    var auditLogSaveChangesInterceptor =
                        serviceProvider.GetRequiredService<AuditLogSaveChangesInterceptor>();
                    var slowQueryDetectionInterceptor =
                        serviceProvider.GetRequiredService<SlowQueryDetectionInterceptor>();

                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        sql =>
                        {
                            sql.CommandTimeout(databaseSettings.CommandTimeout);
                            sql.EnableRetryOnFailure();
                        }
                    );
                    options.AddInterceptors(
                        auditLogSaveChangesInterceptor,
                        slowQueryDetectionInterceptor
                    );

                    if (databaseSettings.EnableSensitiveDataLogging)
                    {
                        options
                            .EnableSensitiveDataLogging()
                            .LogTo(message => logger.LogInformation(message), LogLevel.Information);
                    }
                }
            );

            services.AddScoped<IEtlContext>(provider => provider.GetRequiredService<EtlContext>());

            return services;
        }
    }
}
