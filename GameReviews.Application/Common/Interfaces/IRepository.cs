using GameReviews.Domain.Common;
namespace GameReviews.Application.Common.Interfaces;

public interface IRepository<TEntity, TEntityId>
    where TEntity : BaseEntity<TEntityId>
    where TEntityId : class
{
    public Task<TEntity> AddAsync(TEntity entity);
    public TEntity Update(TEntity entity);
    void Remove(TEntity entity);
    Task<TEntity?> GetByIdAsync(TEntityId id);
}

