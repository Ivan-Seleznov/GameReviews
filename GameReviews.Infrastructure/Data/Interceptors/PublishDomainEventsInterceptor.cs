using GameReviews.Domain.Common.Abstractions.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GameReviews.Infrastructure.Data.Interceptors;

internal sealed class PublishDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _publisher;

    public PublishDomainEventsInterceptor(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        if (eventData.Context is null)
        {
            return base.SavedChanges(eventData, result);
        }
        
        var events = GetDomainEvents(eventData.Context.ChangeTracker);
        var savedRes = base.SavedChanges(eventData, result);
        PublishDomainEventsAsync(events).GetAwaiter().GetResult();
        return savedRes;
    }
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context is null)
        {
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        var events = GetDomainEvents(eventData.Context.ChangeTracker);
        var savedRes = await base.SavedChangesAsync(eventData, result, cancellationToken);
        await PublishDomainEventsAsync(events, cancellationToken);
        return savedRes;
    }
    
    private List<DomainEvent> GetDomainEvents(ChangeTracker changeTracker)
    {
        return changeTracker.Entries<IDomainEntity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .SelectMany(e =>
            {
                var domainEvents = e.DomainEvents;
                e.ClearDomainEvents();

                return domainEvents;
            }).ToList();
    }
    private async Task PublishDomainEventsAsync(List<DomainEvent> events, CancellationToken cancellationToken = default)
    {
        foreach (var ev in events)
        {
            await _publisher.Publish(ev, cancellationToken);
        }
    }
}