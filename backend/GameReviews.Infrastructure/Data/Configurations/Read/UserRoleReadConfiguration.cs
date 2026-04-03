using GameReviews.Application.Common.Models.ReadEntities;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations.Read;

internal sealed class UserRoleReadConfiguration : IEntityTypeConfiguration<UserRoleReadEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleReadEntity> builder)
    {
        builder.ToTable(TableNames.UserRole);
        
        builder.HasKey(ur => new { ur.UsersId, ur.RolesId });
        
        builder
            .HasOne(ur => ur.User)
            .WithMany()
            .HasForeignKey(ur => ur.UsersId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(ur => ur.Role)
            .WithMany()
            .HasForeignKey(ur => ur.RolesId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}