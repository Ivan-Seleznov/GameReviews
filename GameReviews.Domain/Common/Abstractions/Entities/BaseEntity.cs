namespace GameReviews.Domain.Common.Abstractions.Entities;
public abstract class BaseEntity<TEntityId>
    where TEntityId : IEquatable<TEntityId>
{
    public TEntityId Id { get; set; }
}