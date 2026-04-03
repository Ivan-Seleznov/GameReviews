using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Domain.Entities.UserGameRelationAggregate.Entities;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations.Write;
internal sealed class GameUserConfiguration : IEntityTypeConfiguration<GameUserRelationship>
{
    public void Configure(EntityTypeBuilder<GameUserRelationship> builder)
    {
        builder.ToTable(TableNames.UserGame);

        builder.HasKey(x => new { x.GamesId, x.UsersId });

        builder.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(ug => ug.UsersId);

        builder.HasOne<GameEntity>()
            .WithMany()
            .HasForeignKey(ug => ug.GamesId);
        
        builder.Property(x => x.GamesId)
            .HasConversion(x => x.Value, v => new GameId(v));
        builder.Property(x => x.UsersId)
            .HasConversion(x => x.Value, v => new UserId(v));
    }
}