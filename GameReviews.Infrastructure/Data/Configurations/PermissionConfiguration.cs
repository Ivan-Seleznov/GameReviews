using GameReviews.Domain.Common.Authorization;
using GameReviews.Domain.Entities.Roles;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations;
internal class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        builder.ToTable(TableNames.Permissions);

        builder.HasKey(p => p.Id);

        builder.HasData(GetPermissionEntities());
    }

    public IEnumerable<PermissionEntity> GetPermissionEntities()
    {
        return Enum.GetValues<Permission>().Select(p => new PermissionEntity
        {
            Id = (int)p,
            Name = p.ToString()
        });
    }
}
