namespace ETLPipelineTool.Api.Extensions
{
    public static class MediatorPipelineExtension
    {
        public static IServiceCollection AddMediatRPipelineConfiguration(
            this IServiceCollection services
        )
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(UnhandledExceptionBehavior<,>)
            );

            return services;
        }
    }
}
