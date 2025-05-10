namespace ETLPipelineTool.IntegrationTest.Common.Extensions
{
    internal static class ServiceProviderExtension
    {
        internal static IQueryable<T> GetEntities<T>(this IServiceProvider provider)
            where T : BaseEntity
        {
            var context = provider.Resolve<IEtlContext>();
            return context.Get<T>();
        }

        internal static async Task<IServiceProvider> AddEntitiesAsync<T>(
            this IServiceProvider provider,
            params T[] entities
        )
            where T : BaseEntity
        {
            var context = provider.Resolve<IEtlContext>();
            await context.AddRangeAsync(entities);
            return provider;
        }

        internal static async Task<IServiceProvider> SaveChangesAsync(
            this IServiceProvider provider
        )
        {
            var context = provider.Resolve<IEtlContext>();
            await context.SaveChangesAsync();
            return provider;
        }

        internal static async Task<IServiceProvider> SaveChangesNoAuditAsync(
            this IServiceProvider provider
        )
        {
            var context = provider.Resolve<IEtlContext>();
            await context.SaveChangesNoAuditAsync();
            return provider;
        }

        internal static Task ClearChangesAsync(this IServiceProvider provider)
        {
            var context = provider.Resolve<IEtlContext>();
            return context.ClearChangeTracker();
        }

        internal static T Resolve<T>(this IServiceProvider provider)
        {
            var service = provider.GetService<T>();
            return service is null
                ? throw new ArgumentException(
                    "Service {Name} is not found in container. Please inject it through override 'CustomRegister' or in 'BaseTest' class"
                )
                : service;
        }
    }
}
