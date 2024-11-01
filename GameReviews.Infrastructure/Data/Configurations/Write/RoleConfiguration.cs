using GameReviews.Domain.Entities.PermissionAggregate.Entities;
using GameReviews.Domain.Entities.RolePermissionAggregate.Entities;
using GameReviews.Domain.Entities.RolesAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations.Write;
internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Roles);

        builder.HasKey(r => r.Id);

        builder.HasMany<PermissionEntity>()
            .WithMany()
            .UsingEntity<RolePermission>();
        
        /*builder.HasMany<UserEntity>()
            .WithMany();*/
        
        //seed data
        builder.HasData(Role.GetValues());
    }
}