namespace GameReviews.Domain.Common.Abstractions.Entities;
public abstract class BaseDomainEntity<TEntityId> : BaseEntity<TEntityId>, IDomainEntity
    where TEntityId : IEquatable<TEntityId>
{
    private readonly List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.ToList();

    public void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}