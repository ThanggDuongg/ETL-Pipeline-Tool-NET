namespace ETLPipelineTool.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<TDto> GetByIdAsync<TDto>(
            Guid id,
            Expression<Func<TEntity, bool>>? predicate,
            Expression<Func<TEntity, TDto>> selector,
            CancellationToken cancellationToken = default
        )
            where TDto : class;

        IQueryable<TEntity> GetList(bool isTracking = false);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(TEntity entity);

        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
