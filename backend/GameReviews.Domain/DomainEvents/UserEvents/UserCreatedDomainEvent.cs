using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Entities.UserAggregate;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Domain.DomainEvents.UserEvents;

public record UserCreatedDomainEvent(UserEntity User) : DomainEvent;