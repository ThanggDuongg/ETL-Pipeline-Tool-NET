namespace ETLPipelineTool.Infrastructure.Persistence
{
    public interface IEtlContext
    {
        IQueryable<T> Get<T>()
            where T : class, IIdentity<Guid>;

        IQueryable<T> GetTemporalAll<T>()
            where T : class, IIdentity<Guid>;

        Task<T> GetById<T>(Guid id, CancellationToken cancellationToken = default)
            where T : class, IIdentity<Guid>;

        ValueTask<EntityEntry<T>> AddAsync<T>(
            T entity,
            CancellationToken cancellationToken = default
        )
            where T : class;

        Task AddRangeAsync<T>(
            IEnumerable<T> entities,
            CancellationToken cancellationToken = default
        )
            where T : class, IIdentity<Guid>;

        Task<EntityEntry<T>> UpdateAsync<T>(T entity)
            where T : class, IIdentity<Guid>;

        Task UpdateRangeAsync<T>(IEnumerable<T> entities)
            where T : class, IIdentity<Guid>;

        Task<EntityEntry<T>> RemoveAsync<T>(T entity)
            where T : class, IIdentity<Guid>;

        Task RemoveRangeAsync<T>(IEnumerable<T> entities)
            where T : class, IIdentity<Guid>;

        Task<int> SaveChangesAsync(
            bool auditForAddOnly = false,
            CancellationToken cancellationToken = default
        );

        Task<int> SaveChangesNoAuditAsync(CancellationToken cancellationToken = default);

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task BulkInsertAsync<T>(
            IEnumerable<T> entities,
            Action<BulkConfig>? bulkConfigAction = null
        )
            where T : class, IIdentity<Guid>;

        Task MigrateAsync(string? targetMigration = null);

        Task ClearChangeTracker();

        IEnumerable<EntityEntry<T>> Entries<T>()
            where T : class, IIdentity<Guid>;

        bool IsDatabaseExist { get; }

        DbConnection GetDatabaseConnection();
    }
}
