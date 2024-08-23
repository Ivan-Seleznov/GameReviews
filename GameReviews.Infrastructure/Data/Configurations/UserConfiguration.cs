using GameReviews.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameReviews.Infrastructure.Data.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder.Ignore(u => u.DomainEvents);

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

