using GameReviews.Domain.Entities.Roles;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations;
internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Roles);

        builder.HasKey(r => r.Id);

        builder.HasMany(r => r.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>(); ;

        builder.HasMany(r => r.Users)
            .WithMany(r => r.Roles);

        //seed data
        builder.HasData(Role.GetValues());
    }
}