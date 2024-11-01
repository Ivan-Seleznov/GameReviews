using GameReviews.Application.Common.Models.ReadEntities;
using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations.Read;
internal class GameUserReadConfiguration : IEntityTypeConfiguration<GameUserReadEntity>
{
    public void Configure(EntityTypeBuilder<GameUserReadEntity> builder)
    {
        builder.ToTable(TableNames.UsersGames);

        builder.HasKey(x => new { x.UsersId, GameId = x.GamesId });

        builder.Property(x => x.GamesId)
            .HasConversion(x => x.Value, v => new GameId(v));
        builder.Property(x => x.UsersId)
            .HasConversion(x => x.Value, v => new UserId(v));
    }
}