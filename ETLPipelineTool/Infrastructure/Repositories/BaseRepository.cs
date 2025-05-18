namespace ETLPipelineTool.Infrastructure.Repositories
{
    public class BaseRepository<TEntity>(IEtlContext etlContext) : IBaseRepository<TEntity>
        where TEntity : class, IIdentity<Guid>
    {
        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await etlContext.AddAsync<TEntity>(entity, cancellationToken);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await etlContext.RemoveAsync<TEntity>(entity);
        }

        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            await DeleteAsync(entity);
        }

        public async Task<TEntity> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            return await etlContext.GetById<TEntity>(id, cancellationToken);
        }

        public async Task<TDto> GetByIdAsync<TDto>(
            Guid id,
            Expression<Func<TEntity, bool>>? predicate,
            Expression<Func<TEntity, TDto>> selector,
            CancellationToken cancellationToken = default
        )
            where TDto : class
        {
            var queryable = etlContext.Get<TEntity>().Where(e => EF.Property<Guid>(e, "Id") == id);

            if (predicate is not null)
            {
                queryable = queryable.Where(predicate);
            }

            var data =
                await queryable.Select(selector).SingleOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException<Guid>(typeof(TEntity), id);

            return data;
        }

        public async Task<TEntity> GetByIdAsync(
            Guid id,
            Expression<Func<TEntity, bool>>? predicate,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
            bool isTracking = false,
            CancellationToken cancellationToken = default
        )
        {
            var query = etlContext.Get<TEntity>();

            if (isTracking)
            {
                query = query.AsTracking();
            }
            if (include != null)
            {
                query = include(query);
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken)
                ?? throw new NotFoundException<Guid>(typeof(TEntity), id);
        }

        public IQueryable<TEntity> GetList(bool isTracking = false)
        {
            var query = etlContext.Get<TEntity>();
            if (isTracking)
            {
                query = query.AsTracking();
            }

            return query;
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await etlContext.UpdateAsync<TEntity>(entity);
        }
    }
}
