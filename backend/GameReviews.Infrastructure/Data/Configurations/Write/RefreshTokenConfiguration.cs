using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations.Write;
internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.ToTable(TableNames.RefreshTokens);
        
        builder.HasKey(r => r.Id);

        builder.HasOne<UserEntity>() 
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
