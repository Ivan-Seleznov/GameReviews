using GameReviews.Application.Common.Models.ReadEntities;
using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations.Read;
internal class GameReadConfiguration : IEntityTypeConfiguration<GameReadEntity>
{
    public void Configure(EntityTypeBuilder<GameReadEntity> builder)
    {
        builder.ToTable(TableNames.Games);

        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Users)
            .WithMany()
            .UsingEntity<GameUserReadEntity>();

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