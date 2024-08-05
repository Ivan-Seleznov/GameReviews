﻿using System.Reflection;
using System.Threading;
using GameReviews.Domain.Common;
using GameReviews.Domain.Entities.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
        var domainEvents = GetDomainEvents();
        var result = base.SaveChanges();
        PublishDomainEventsAsync(domainEvents).GetAwaiter().GetResult();

        return result;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var domainEvents = GetDomainEvents();
        var result = await base.SaveChangesAsync(cancellationToken);
        await PublishDomainEventsAsync(domainEvents,cancellationToken);
        
        return result;
    }

    private List<DomainEvent> GetDomainEvents()
    {
        return ChangeTracker.Entries<IEntity>()
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