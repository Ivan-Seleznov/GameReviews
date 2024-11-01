using System.Data;
using System.Data.Common;
using GameReviews.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GameReviews.Infrastructure.Data;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationWriteDbContext _context;
    public UnitOfWork(ApplicationWriteDbContext context)
    {
        _context = context;
    }
    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<DbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken)
    {
        var transaction = await _context.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        return transaction.GetDbTransaction();
    }

    public IExecutionStrategy CreateExecutionStrategy() =>
        _context.Database.CreateExecutionStrategy();

    public IDbContextTransaction? GetCurrentTransaction() => 
        _context.Database.CurrentTransaction;
}