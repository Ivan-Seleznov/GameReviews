using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.DomainEvents.UserEvents;
using MediatR;

namespace GameReviews.Application.Common.Interfaces.Events;

public interface IDomainEventHandler<in TNotification> : INotificationHandler<TNotification>
    where TNotification : DomainEvent;