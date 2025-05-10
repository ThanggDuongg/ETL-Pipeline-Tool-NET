namespace ETLPipelineTool.IntegrationTest.Common.Extensions
{
    internal static class ServiceCollectionExtension
    {
        internal static IServiceCollection AddHandlers(
            this IServiceCollection services,
            Assembly? assembly = null
        )
        {
            var usedAssembly = assembly ?? Assembly.GetExecutingAssembly();
            GetTypesAssignableTo(usedAssembly, typeof(IRequestHandler<,>))
                .ForEach(
                    (type) =>
                    {
                        foreach (var implInterface in type.ImplementedInterfaces)
                        {
                            services.AddScoped(implInterface, type);
                        }
                    }
                );
            return services;
        }

        internal static IServiceCollection AddValidators(
            this IServiceCollection services,
            Assembly? assembly = null
        )
        {
            var usedAssembly = assembly ?? Assembly.GetExecutingAssembly();
            GetTypesAssignableTo(usedAssembly, typeof(IValidator<>))
                .ForEach(
                    (type) =>
                    {
                        foreach (var implInterface in type.ImplementedInterfaces)
                        {
                            services.AddScoped(implInterface, type);
                        }
                    }
                );
            return services;
        }

        private static List<TypeInfo> GetTypesAssignableTo(Assembly assembly, Type compareType)
        {
            return
            [
                .. assembly.DefinedTypes.Where(x =>
                    x.IsClass
                    && !x.IsAbstract
                    && x != compareType
                    && x.GetInterfaces()
                        .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == compareType)
                ),
            ];
        }
    }
}
