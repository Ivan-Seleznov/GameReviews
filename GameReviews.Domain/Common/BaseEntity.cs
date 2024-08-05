using System.ComponentModel.DataAnnotations.Schema;

namespace GameReviews.Domain.Common;
public class BaseEntity<TEntityId> 
    : IEntity
    where TEntityId : class
{
    private readonly List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.ToList();

    public TEntityId Id { get; set; }

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