using GameReviews.Domain.Common;
using GameReviews.Domain.DomainEvents.UserEvents;
using MediatR;

namespace GameReviews.Application.Common.Interfaces;

public interface IDomainEventHandler<in TNotification> : INotificationHandler<TNotification>
    where TNotification : DomainEvent;