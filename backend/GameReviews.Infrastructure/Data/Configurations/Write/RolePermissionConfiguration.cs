using GameReviews.Domain.Common.Authorization;
using GameReviews.Domain.Entities.PermissionAggregate.Entities;
using GameReviews.Domain.Entities.RolePermissionAggregate.Entities;
using GameReviews.Domain.Entities.RolesAggregate.Entities;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations.Write;
internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable(TableNames.RolePermissions);
        
        builder.HasKey(r => new { r.RoleId, PermissionsId = r.PermissionEntityId });
        
        builder.HasData(CreateRolePermissions());
    }

    private static RolePermission[] CreateRolePermissions()
    {
        return
        [
            RolePermission.Create(Role.Registered, Permission.ReadUser),
            RolePermission.Create(Role.Admin,Permission.ManageUser),
            RolePermission.Create(Role.Admin, Permission.ReadUser)
        ];
    }
}
