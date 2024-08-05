namespace GameReviews.Application.Common.Interfaces;
public interface IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken);

    /*
    public Task BeginTransactionAsync(CancellationToken cancellationToken);
    public Task CommitTransactionAsync(CancellationToken cancellationToken);
    public Task RollbackTransactionAsync(CancellationToken cancellationToken);
    */
}
