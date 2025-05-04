using System.Linq.Expressions;

namespace ETLPipelineTool.Infrastructure.Repositories
{
    public class BaseRepository<TEntity>(IEtlContext etlContext) : IBaseRepository<TEntity>
        where TEntity : class, IIdentity<Guid>
    {
        protected readonly IEtlContext EtlContext = etlContext;

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await EtlContext.AddAsync<TEntity>(entity, cancellationToken);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await EtlContext.RemoveAsync<TEntity>(entity);
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
            return await EtlContext.GetById<TEntity>(id, cancellationToken);
        }

        public async Task<TDto> GetByIdAsync<TDto>(
            Guid id,
            Expression<Func<TEntity, bool>>? predicate,
            Expression<Func<TEntity, TDto>> selector,
            CancellationToken cancellationToken = default
        )
            where TDto : class
        {
            var queryable = EtlContext.Get<TEntity>().Where(e => EF.Property<Guid>(e, "Id") == id);

            if (predicate is not null)
            {
                queryable = queryable.Where(predicate);
            }

            var data =
                await queryable.Select(selector).SingleOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException<Guid>(typeof(TEntity), id);

            return data;
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await EtlContext.UpdateAsync<TEntity>(entity);
        }
    }
}
