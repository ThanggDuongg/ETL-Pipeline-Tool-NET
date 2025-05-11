namespace ETLPipelineTool.Shared.Helpers
{
    public static class EnvironmentsHelper
    {
        public static void SetupSerilog(WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog(
                (ctx, services, loggerConfig) =>
                {
                    loggerConfig
                        .ReadFrom.Configuration(ctx.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext();
                }
            );
        }

        public static bool IsDevelopment()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        }
    }
}
