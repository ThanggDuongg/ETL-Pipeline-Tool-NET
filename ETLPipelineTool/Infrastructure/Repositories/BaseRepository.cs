namespace ETLPipelineTool.Infrastructure.Repositories
{
    public class BaseRepository<TEntity>(EtlContext etlContext) : IBaseRepository<TEntity, Guid>
        where TEntity : class, IIdentity<Guid>
    {
        protected readonly EtlContext EtlContext = etlContext;

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await EtlContext.AddAsync<TEntity>(entity, cancellationToken);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await EtlContext.RemoveAsync<TEntity>(entity);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            await DeleteAsync(entity);
        }

        public async Task<TEntity> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            return await EtlContext.GetById<TEntity>(id, cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await EtlContext.UpdateAsync<TEntity>(entity);
        }
    }
}
