using GameReviews.Application.Common.Models.ReadEntities;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations.Read;
internal class RoleReadConfiguration : IEntityTypeConfiguration<RoleReadEntity>
{
    public void Configure(EntityTypeBuilder<RoleReadEntity> builder)
    {
        builder.ToTable(TableNames.Roles);

        builder.HasKey(r => r.Id);
        
        builder.HasMany(r => r.Permissions)
            .WithMany()
            .UsingEntity<RolePermissionReadEntity>();
        
        builder.HasMany(r => r.Users)
            .WithMany(r => r.Roles)
            .UsingEntity<UserRoleReadEntity>();
    }
}