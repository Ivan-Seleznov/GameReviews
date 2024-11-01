using GameReviews.Application.Common.Models.ReadEntities;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations.Read;

internal class UserReadConfiguration : IEntityTypeConfiguration<UserReadEntity>
{
    public void Configure(EntityTypeBuilder<UserReadEntity> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder.HasMany(u => u.Games)
            .WithMany(g => g.Users)
            .UsingEntity<GameUserReadEntity>();
        
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasIndex(u => u.Username)
            .IsUnique();

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(320);
        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}