namespace ETLPipelineTool.Api.Extensions
{
    public static class AppSettingsExtension
    {
        public static IServiceCollection AddAppSettingsConfiguration(
            this IServiceCollection services,
            ConfigurationManager configurationManager
        )
        {
            services.Configure<DatabaseSettings>(configurationManager.GetSection("Database"));
            return services;
        }
    }
}
