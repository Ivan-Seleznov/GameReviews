using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Domain.Entities.ReviewAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Domain.Entities.UserGameRelationAggregate.Entities;
using GameReviews.Infrastructure.Data.Configurations.Write;
using GameReviews.Infrastructure.Data.Converters;
using GameReviews.Infrastructure.Data.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Infrastructure.Data;
public class ApplicationWriteDbContext : DbContext
{
    private readonly IPublisher _publisher;

    public ApplicationWriteDbContext(DbContextOptions<ApplicationWriteDbContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<GameEntity> Games { get; set; }
    public DbSet<ReviewEntity> Reviews { get; set; }
    public DbSet<GameUserRelationship> UsersGames { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(IGameReviewsInfrastructureMarker).Assembly,
            WriteConfigurationsFilter);
        
        modelBuilder.ApplyValueConverters(
            typeof(EntityId<int>), 
            typeof(IntToBaseEntityIdConverter<>),
            x => x.HasIdentityOptions(1, 1));
        
        modelBuilder.ApplyValueConverters(
            typeof(EntityId<long>), 
            typeof(LongToBaseEntityIdConverter<>),
            x => x.HasIdentityOptions(1, 1));
    }
    private static bool WriteConfigurationsFilter(Type type) =>
        type.FullName?.Contains("Configurations.Write") ?? false;
}