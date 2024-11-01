using GameReviews.Domain.Common.Errors;
using GameReviews.Domain.Results;
using GameReviews.Domain.Results.Errors;

namespace GameReviews.Domain.Common.Abstractions.Entities;
public abstract class BaseEntity<TEntityId> : IDomainEntity
    where TEntityId : IEquatable<TEntityId>
{
    public TEntityId Id { get; protected set; }
    
    private readonly List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.ToList();
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}