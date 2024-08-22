namespace GameReviews.Domain.Common;
public abstract class BaseEntity<TEntityId>
    where TEntityId : IEquatable<TEntityId>
{
    public TEntityId Id { get; set; }
}
