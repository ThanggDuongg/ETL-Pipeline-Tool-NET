namespace ETLPipelineTool.IntegrationTest
{
    public abstract class BaseIntegrationTest : IDisposable
    {
        private bool _disposed = false;

        protected readonly IServiceProvider Container;
        private readonly IServiceScope _scope;
        protected readonly EtlContext Context;

        // Setup
        protected BaseIntegrationTest()
        {
            var services = new ServiceCollection();
            var assembly = Assembly.GetAssembly(typeof(Program))!;

            // MediatR
            services.AddHandlers(assembly).AddValidators(assembly);
            services.AddApplicationServices();
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(assembly);
            });

            // Services
            services.AddScoped(typeof(Lazy<>));

            services.AddDbContext<EtlContext>(options =>
            {
                options
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .EnableSensitiveDataLogging();
            });
            services.AddScoped<IEtlContext>(provider => provider.GetRequiredService<EtlContext>());
            services.AddInfrastructureServices();
            // Custom services or mock
            CustomRegister(services);
            Container = services.BuildServiceProvider();

            _scope = Container.CreateScope();
            Context = _scope.ServiceProvider.GetRequiredService<EtlContext>();
            Context.Database.EnsureCreated();
        }

        protected virtual IServiceCollection CustomRegister(IServiceCollection services)
        {
            return services;
        }

        // Tear down
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Database.EnsureDeleted();
                    _scope.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    public abstract class BaseIntegrationTest<T> : BaseIntegrationTest
        where T : class
    {
        protected T Service { get; }

        protected BaseIntegrationTest()
        {
            Service = Container.GetRequiredService<T>();
        }
    }

    public abstract class BaseValidatorTest<T> : BaseIntegrationTest
        where T : class
    {
        protected IEnumerable<IValidator<T>> Services { get; }

        protected BaseValidatorTest()
        {
            Services = Container.GetRequiredService<IEnumerable<IValidator<T>>>();
        }

        protected async Task<ValidationFailure[]> ValidateAsync(T request)
        {
            var context = new ValidationContext<T>(request);

            var validationResults = await Task.WhenAll(
                Services.Select(x => x.ValidateAsync(context))
            );
            return validationResults.SelectMany(x => x.Errors).Where(x => x != null).ToArray();
        }
    }

    public abstract class BaseHandlerTest<TRequest, TResponse> : BaseIntegrationTest
        where TRequest : class, IRequest<TResponse>
    {
        protected IRequestHandler<TRequest, TResponse> Service { get; }

        protected BaseHandlerTest()
        {
            Service = Container.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        }
    }
}
