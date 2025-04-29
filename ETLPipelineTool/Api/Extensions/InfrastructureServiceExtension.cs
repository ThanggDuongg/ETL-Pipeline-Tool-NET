namespace ETLPipelineTool.Api.Extensions
{
    public static class InfrastructureServiceExtension
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IBookRepository, BookRepository>();
            //services.AddScoped<IAuditLogger, AuditLogger>();

            return services;
        }
    }
}
