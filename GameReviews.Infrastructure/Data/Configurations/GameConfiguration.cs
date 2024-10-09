using GameReviews.Domain.Entities.Game;
using GameReviews.Domain.Entities.UserGame;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations;
internal class GameConfiguration : IEntityTypeConfiguration<GameEntity>
{
    public void Configure(EntityTypeBuilder<GameEntity> builder)
    {
        builder.ToTable(TableNames.Games);

        builder.HasKey(x => x.Id);

        /*builder.HasMany(x => x.Users)
            .WithMany()
            .UsingEntity<GameEntityUserEntity>();*/

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, l => new GameId(l))
            .IsRequired()
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1500);
    }
}