using GameReviews.Application.Common.Interfaces;
using GameReviews.Domain.DomainEvents.UserEvents;
using Microsoft.Extensions.Logging;

namespace GameReviews.Application.Users.EventHandlers;
public class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
{
    private readonly ILogger<UserCreatedDomainEventHandler> _logger;
    public UserCreatedDomainEventHandler(ILogger<UserCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        //template for future UserCreatedDomainEvent handler
        _logger.LogInformation("UserCreated DomainEvent handler executed");

        return Task.CompletedTask;
    }
}

