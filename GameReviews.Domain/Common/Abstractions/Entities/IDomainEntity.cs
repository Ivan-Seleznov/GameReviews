namespace GameReviews.Domain.Common.Abstractions.Entities;
public interface IDomainEntity
{
    public IReadOnlyCollection<DomainEvent> DomainEvents { get; }

    public void AddDomainEvent(DomainEvent domainEvent);
    public void RemoveDomainEvent(DomainEvent domainEvent);
    public void ClearDomainEvents();
}