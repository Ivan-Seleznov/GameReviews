using GameReviews.Application.Common.Interfaces;

namespace GameReviews.Infrastructure.Data;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync();
    }

    /*
    public Task BeginTransactionAsync(CancellationToken cancellationToken)
    { 
        return _context.Database.BeginTransactionAsync();
    }
    public Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        return _context.Database.CommitTransactionAsync();
    }
    public Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        return _context.Database.RollbackTransactionAsync();
    }
    */
}

