using GameReviews.Domain.Common.Authorization;
using GameReviews.Domain.Entities.PermissionAggregate.Entities;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations.Write;
internal sealed class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        builder.ToTable(TableNames.Permissions);

        builder.HasKey(p => p.Id);
    
        builder.HasData(GetPermissionEntities());
    }

    private IEnumerable<PermissionEntity> GetPermissionEntities()
    {
        return Enum.GetValues<Permission>().Select(p => PermissionEntity.Create((int)p, p.ToString()));
    }
}