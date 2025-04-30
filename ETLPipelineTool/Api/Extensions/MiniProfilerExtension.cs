namespace ETLPipelineTool.Api.Extensions
{
    public static class MiniProfilerExtension
    {
        public static IServiceCollection AddMiniProfilerSupport(this IServiceCollection services)
        {
            services
                .AddMiniProfiler(options =>
                {
                    options.RouteBasePath = "/profiler";
                    options.TrackConnectionOpenClose = true;
                    options.EnableServerTimingHeader = true;

                    if (options.Storage is MemoryCacheStorage memoryCacheStorage)
                    {
                        memoryCacheStorage.CacheDuration = TimeSpan.FromMinutes(60);
                    }
                    options.SqlFormatter = new InlineFormatter();
                    options.ColorScheme = StackExchange.Profiling.ColorScheme.Auto;
                })
                .AddEntityFramework();
            return services;
        }
    }
}
