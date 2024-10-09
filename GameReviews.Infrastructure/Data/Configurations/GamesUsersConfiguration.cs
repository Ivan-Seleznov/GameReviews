using GameReviews.Domain.Entities.Game;
using GameReviews.Domain.Entities.User;
using GameReviews.Domain.Entities.UserGame;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations;
internal class GamesUsersConfiguration : IEntityTypeConfiguration<GameEntityUserEntity>
{
    public void Configure(EntityTypeBuilder<GameEntityUserEntity> builder)
    {
        builder.ToTable(TableNames.UsersGames);

        builder.HasKey(x => new { x.UsersId, GameId = x.GamesId });

        builder.Property(x => x.GamesId)
            .HasConversion(x => x.Value, v => new GameId(v));
        builder.Property(x => x.UsersId)
            .HasConversion(x => x.Value, v => new UserId(v));
    }
}