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

            // DbContexts
            services.AddDbContext<EtlContext>(
                (serviceProvider, options) =>
                {
                    var databaseSettings = serviceProvider
                        .GetRequiredService<IOptions<DatabaseSettings>>()
                        .Value;
                    var auditLogSaveChangesInterceptor =
                        serviceProvider.GetRequiredService<AuditLogSaveChangesInterceptor>();

                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        sql =>
                        {
                            sql.CommandTimeout(databaseSettings.CommandTimeout);
                            sql.EnableRetryOnFailure();
                        }
                    );
                    options.AddInterceptors(auditLogSaveChangesInterceptor);

                    if (databaseSettings.EnableSensitiveDataLogging)
                    {
                        options.EnableSensitiveDataLogging();
                    }
                }
            );

            return services;
        }
    }
}
