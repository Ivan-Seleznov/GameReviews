using GameReviews.Application.Common.Models.ReadEntities;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations.Read;
internal class PermissionReadConfiguration : IEntityTypeConfiguration<PermissionReadEntity>
{
    public void Configure(EntityTypeBuilder<PermissionReadEntity> builder)
    {
        builder.ToTable(TableNames.Permissions);
        
        builder.HasKey(p => p.Id);
    }
}