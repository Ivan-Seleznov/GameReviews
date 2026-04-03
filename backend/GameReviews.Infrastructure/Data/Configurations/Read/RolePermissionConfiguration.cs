using GameReviews.Application.Common.Models.ReadEntities;
using GameReviews.Domain.Entities.RolesAggregate.Entities;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations.Read;
internal class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionReadEntity>
{
    public void Configure(EntityTypeBuilder<RolePermissionReadEntity> builder)
    {
        builder.ToTable(TableNames.RolePermission);
        
        builder.HasKey(r => new { r.RolesId, r.PermissionsId });

        builder.HasOne<RoleReadEntity>()
            .WithMany()
            .HasForeignKey(r => r.RolesId);
        builder.HasOne<PermissionReadEntity>()
            .WithMany()
            .HasForeignKey(r => r.PermissionsId);
    }
}
