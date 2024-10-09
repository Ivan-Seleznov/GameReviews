using GameReviews.Domain.Entities.RefreshToken;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations;
internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasOne(r=> r.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(r => r.Id)
            .HasConversion(userId => userId.Value,
                value => new(value))
            .ValueGeneratedOnAdd()
            .HasIdentityOptions(1, 1);
    }
}
