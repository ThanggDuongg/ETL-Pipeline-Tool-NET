namespace ETLPipelineTool.Api.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SomeCommandHandler).Assembly));
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //services.AddScoped<IPipelineBehavior<,>, ValidationBehavior<,>>();
            //services.AddScoped<IPipelineBehavior<,>, LoggingBehavior<,>>();

            return services;
        }
    }
}
