using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace GameReviews.Application.Common.Interfaces;
public interface IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken);
    public Task<DbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel,
        CancellationToken cancellationToken);
    public IExecutionStrategy CreateExecutionStrategy();
    public IDbContextTransaction? GetCurrentTransaction();
}