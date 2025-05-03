namespace ETLPipelineTool.Api.Extensions
{
    public static class ValidationExtension
    {
        public static IServiceCollection AddValidationConfiguration(
            this IServiceCollection services
        )
        {
            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .Configure<ApiBehaviorOptions>(options =>
                {
                    //options.SuppressModelStateInvalidFilter = true; // Bypass validate model state
                    options.InvalidModelStateResponseFactory = (context) =>
                    {
                        return new BadRequestObjectResult("Invalid request");
                    };
                });

            return services;
        }
    }
}
