using GameReviews.Domain.Results;
using GameReviews.Domain.Results.Errors;

namespace GameReviews.Domain.Common.Abstractions.Entities;
public abstract class BaseEntity<TEntityId> : IDomainEntity
    where TEntityId : IEquatable<TEntityId>
{
    public TEntityId Id { get; set; }
    
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
    
    protected Result CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            Result.Failure(DomainErrors.RuleBroken(rule.Message));
        }
        
        return Result.Success();
    }
}