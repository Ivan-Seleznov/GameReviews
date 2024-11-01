using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Models.ReadEntities;
using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Infrastructure.Data.Converters;
using GameReviews.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Infrastructure.Data;
public class ApplicationReadDbContext : DbContext, IReadApplicationDbContext
{
    public ApplicationReadDbContext(DbContextOptions<ApplicationReadDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserReadEntity> Users { get; set; }
    public DbSet<GameReadEntity> Games { get; set; }
    public DbSet<ReviewReadEntity> Reviews { get; set; }
    public DbSet<GameUserReadEntity> UsersGames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(IGameReviewsInfrastructureMarker).Assembly,
            ReadConfigurationsFilter);
        
        modelBuilder.ApplyValueConverters(
            typeof(EntityId<int>), 
            typeof(IntToBaseEntityIdConverter<>),
            x => x.HasIdentityOptions(1, 1));
    }
    private static bool ReadConfigurationsFilter(Type type) =>
        type.FullName?.Contains("Configurations.Read") ?? false;
}