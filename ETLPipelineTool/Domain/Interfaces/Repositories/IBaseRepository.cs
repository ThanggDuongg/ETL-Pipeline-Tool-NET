namespace ETLPipelineTool.Domain.Interfaces.Repositories
{
  public interface IBaseRepository<TEntity>
    where TEntity : class
  {
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<TEntity> GetByIdAsync(
      Guid id,
      Expression<Func<TEntity, bool>>? predicate,
      Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null,
      bool isTracking = false,
      bool asSplitQuery = false,
      CancellationToken cancellationToken = default
    );

    Task<TDto> GetByIdAsync<TDto>(
      Guid id,
      Expression<Func<TEntity, bool>>? predicate,
      Expression<Func<TEntity, TDto>> selector,
      CancellationToken cancellationToken = default
    )
      where TDto : class;

    IQueryable<TEntity> Get(bool isTracking = false);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task AddRangeAsync(
      IEnumerable<TEntity> entities,
      CancellationToken cancellationToken = default
    );

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(TEntity entity);

    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
  }
}
