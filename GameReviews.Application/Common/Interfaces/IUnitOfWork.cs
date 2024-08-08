using System.Data;
using System.Data.Common;

namespace GameReviews.Application.Common.Interfaces;
public interface IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken);
    public Task<DbTransaction> BeginTransactionAsync(IsolationLevel isolationLevel,
        CancellationToken cancellationToken);
}