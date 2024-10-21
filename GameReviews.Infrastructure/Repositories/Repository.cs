using GameReviews.Application.Common.Interfaces.Repositories;
using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Infrastructure.Repositories;

public abstract class Repository<TEntity, TEntityId>(ApplicationDbContext context) :
    IRepository<TEntity, TEntityId>
    where TEntity : BaseEntity<TEntityId>
    where TEntityId : IEquatable<TEntityId>
{
    private readonly ApplicationDbContext _context = context;

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entityRes = await _context.Set<TEntity>().AddAsync(entity);
        return entityRes.Entity;
    }

    public TEntity Update(TEntity entity)
    {
        return _context.Set<TEntity>().Update(entity).Entity;
    }

    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public async Task<TEntity?> GetByIdAsync(TEntityId id)
    {
        return await _context.Set<TEntity>().SingleOrDefaultAsync(e => e.Id.Equals(id));
    }

    public async Task<TEntity?> GetByIdNoTrackingAsync(TEntityId id)
    {
        return await _context.Set<TEntity>()
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id.Equals(id));
    }

    public async Task<bool> ExistsAsync(TEntityId id)
    {
        return await _context.Set<TEntity>().AnyAsync(e => e.Id.Equals(id));
    }

    public Task<List<TEntity?>> GetAllAsync(int page, int size)
    {
        throw new NotImplementedException();
    }
}