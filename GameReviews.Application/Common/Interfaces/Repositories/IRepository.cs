using GameReviews.Domain.Common;
namespace GameReviews.Application.Common.Interfaces.Repositories;

public interface IRepository<TEntity, TEntityId>
    where TEntityId : IEquatable<TEntityId>
    where TEntity : BaseEntity<TEntityId>
{
    public Task<TEntity> AddAsync(TEntity entity);
    public TEntity Update(TEntity entity);
    void Remove(TEntity entity);
    Task<TEntity?> GetByIdAsync(TEntityId id);
}