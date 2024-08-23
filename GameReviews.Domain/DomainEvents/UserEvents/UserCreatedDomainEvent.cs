using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Domain.DomainEvents.UserEvents;

public record UserCreatedDomainEvent(UserEntity User) : DomainEvent;