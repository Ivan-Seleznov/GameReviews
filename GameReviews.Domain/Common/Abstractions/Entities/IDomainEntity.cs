namespace GameReviews.Domain.Common.Abstractions.Entities;
public interface IDomainEntity
{
    public IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    public void ClearDomainEvents();
}