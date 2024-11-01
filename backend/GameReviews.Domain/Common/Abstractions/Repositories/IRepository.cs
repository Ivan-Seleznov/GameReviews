using GameReviews.Domain.Common.Abstractions.Entities;

namespace GameReviews.Domain.Common.Abstractions.Repositories;
public interface IRepository<TEntity, in TEntityId>
    where TEntityId : IEquatable<TEntityId>
    where TEntity : BaseEntity<TEntityId>
{
    public Task<TEntity> AddAsync(TEntity entity);
    public TEntity Update(TEntity entity);
    void Remove(TEntity entity);
    Task<TEntity?> GetByIdAsync(TEntityId id);
    Task<TEntity?> GetByIdNoTrackingAsync(TEntityId id);
    Task<bool> ExistsAsync(TEntityId id);
}