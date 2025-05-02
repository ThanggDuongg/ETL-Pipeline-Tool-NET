namespace ETLPipelineTool.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity, TIdentity>
        where TEntity : class, IIdentity<TIdentity>
    {
        Task<TEntity> GetByIdAsync(TIdentity id, CancellationToken cancellationToken = default);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(TEntity entity);

        Task DeleteAsync(TIdentity id, CancellationToken cancellationToken = default);
    }
}
