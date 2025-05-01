namespace ETLPipelineTool.Infrastructure.Persistence
{
    public class EtlContext(DbContextOptions<EtlContext> options) : DbContext(options), IEtlContext
    {
        public DbSet<EtlPipeline> EtlPipelines => Set<EtlPipeline>();
        public DbSet<PipelineSchedule> PipelineSchedules => Set<PipelineSchedule>();
        public DbSet<FieldMapping> FieldMappings => Set<FieldMapping>();
        public DbSet<TransformRule> TransformRules => Set<TransformRule>();
        public DbSet<EtlExecutionLog> EtlExecutionLogs => Set<EtlExecutionLog>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigIdentity();
            modelBuilder.ConfigRowVersion();
            modelBuilder.ConfigTableName();

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public bool IsDatabaseExist =>
            ((RelationalDatabaseCreator)Database.GetService<IDatabaseCreator>()).Exists();

        public override async ValueTask<EntityEntry<T>> AddAsync<T>(
            T entity,
            CancellationToken cancellationToken = default
        )
            where T : class
        {
            return await base.AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync<T>(
            IEnumerable<T> entities,
            CancellationToken cancellationToken = default
        )
            where T : class, IIdentity<Guid>
        {
            await base.AddRangeAsync(entities, cancellationToken);
        }

        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return Database.BeginTransactionAsync();
        }

        public async Task BulkInsertAsync<T>(
            IEnumerable<T> entities,
            Action<BulkConfig>? bulkConfigAction = null
        )
            where T : class, IIdentity<Guid>
        {
            var entitiesArray = entities.ToArray();
            await AddAuditData(
                entitiesArray
                    .Where(x => x is IAuditing)
                    .Cast<IAuditing>()
                    .Select(x => (x, EntityState.Added))
            );
            await DbContextBulkExtensions.BulkInsertAsync(this, entitiesArray, bulkConfigAction);
        }

        public Task ClearChangeTracker()
        {
            ChangeTracker.Clear();
            return Task.CompletedTask;
        }

        public IEnumerable<EntityEntry<T>> Entries<T>()
            where T : class, IIdentity<Guid>
        {
            return ChangeTracker.Entries<T>();
        }

        public override DbSet<TEntity> Set<
            [DynamicallyAccessedMembers(
                DynamicallyAccessedMemberTypes.PublicConstructors
                    | DynamicallyAccessedMemberTypes.NonPublicConstructors
                    | DynamicallyAccessedMemberTypes.PublicFields
                    | DynamicallyAccessedMemberTypes.NonPublicFields
                    | DynamicallyAccessedMemberTypes.PublicProperties
                    | DynamicallyAccessedMemberTypes.NonPublicProperties
                    | DynamicallyAccessedMemberTypes.Interfaces
            )]
                TEntity
        >(string name)
        {
            return base.Set<TEntity>(name);
        }

        public IQueryable<T> Get<T>()
            where T : class, IIdentity<Guid>
        {
            return base.Set<T>().AsNoTracking();
        }

        public async Task<T> GetById<T>(Guid id, CancellationToken cancellationToken = default)
            where T : class, IIdentity<Guid>
        {
            var record =
                await Get<T>().SingleOrDefaultAsync(x => x.Id == id, cancellationToken)
                ?? throw new NotFoundException<Guid>(typeof(T), id);

            return record;
        }

        public DbConnection GetDatabaseConnection()
        {
            return Database.GetDbConnection();
        }

        public IQueryable<T> GetTemporalAll<T>()
            where T : class, IIdentity<Guid>
        {
            return base.Set<T>().TemporalAll().AsNoTracking();
        }

        public async Task MigrateAsync(string? targetMigration = null)
        {
            var migrator = Database.GetService<IMigrator>();
            await migrator.MigrateAsync(targetMigration);
        }

        public Task<EntityEntry<T>> RemoveAsync<T>(T entity)
            where T : class, IIdentity<Guid>
        {
            return Task.FromResult(base.Remove(entity));
        }

        public Task RemoveRangeAsync<T>(IEnumerable<T> entities)
            where T : class, IIdentity<Guid>
        {
            base.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public override async Task<int> SaveChangesAsync(
            bool auditForAddOnly = false,
            CancellationToken cancellation = default
        )
        {
            await SetAuditData(auditForAddOnly);
            await AdaptRowVersion();
            return await base.SaveChangesAsync(cancellation);
        }

        public async Task<int> SaveChangesNoAuditAsync(
            CancellationToken cancellationToken = default
        )
        {
            await AdaptRowVersion();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public Task<EntityEntry<T>> UpdateAsync<T>(T entity)
            where T : class, IIdentity<Guid>
        {
            return Task.FromResult(base.Update(entity));
        }

        public Task UpdateRangeAsync<T>(IEnumerable<T> entities)
            where T : class, IIdentity<Guid>
        {
            base.UpdateRange(entities);
            return Task.CompletedTask;
        }

        private Task AdaptRowVersion()
        {
            var auditEntries = ChangeTracker.Entries<IVersioning>();
            foreach (
                var entityEntry in auditEntries.Where(x =>
                    x?.Entity is not null && x.State == EntityState.Modified
                )
            )
            {
                entityEntry.OriginalValues[nameof(IVersioning.RowVersion)] = entityEntry
                    .Entity
                    .RowVersion;
            }
            return Task.CompletedTask;
        }

        private async Task SetAuditData(bool auditForAddOnly = false)
        {
            var entries = ChangeTracker.Entries<IAuditing>();

            if (auditForAddOnly)
            {
                entries = entries.Where(x => x.State == EntityState.Added);
            }

            await AddAuditData(entries.Select(x => (x.Entity, x.State)));
        }

        private static Task AddAuditData(
            IEnumerable<(IAuditing Entity, EntityState State)> entities
        )
        {
            var now = DateTime.UtcNow;
            foreach (var (Entity, State) in entities)
            {
                var entity = Entity;
                if (State == EntityState.Added)
                {
                    entity.CreatedBy = "SYSTEM"; // TODO: Upgrade Auth for system
                    entity.CreatedOn = now;
                    entity.ModifiedBy = entity.CreatedBy;
                    entity.ModifiedOn = entity.CreatedOn;
                }
                else if (State == EntityState.Modified)
                {
                    entity.ModifiedBy = "SYSTEM";
                    entity.ModifiedOn = now;
                }
            }

            return Task.CompletedTask;
        }
    }
}
