using GameReviews.Application.Common.Models.ReadEntities;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations.Read;
internal class RefreshTokenReadConfiguration : IEntityTypeConfiguration<RefreshTokenReadEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenReadEntity> builder)
    {
        builder.ToTable(TableNames.RefreshTokens);
        
        builder.HasKey(r => r.Id);

        builder.HasOne(r => r.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId);
        
        builder.Property(r => r.Id)
            .HasConversion(userId => userId.Value,
                value => new(value))
            .ValueGeneratedOnAdd()
            .HasIdentityOptions(1, 1);
    }
}
