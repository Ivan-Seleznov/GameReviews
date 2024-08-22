using GameReviews.Domain.Common.Authorization;
using GameReviews.Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations;
internal class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(r => new { r.RoleId, r.PermissionsId });

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
