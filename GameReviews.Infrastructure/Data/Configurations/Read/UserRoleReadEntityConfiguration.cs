using GameReviews.Application.Common.Models.ReadEntities;
using GameReviews.Domain.Entities.RolesAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations.Read;

internal sealed class UserRoleReadEntityConfiguration : IEntityTypeConfiguration<UserRoleReadEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleReadEntity> builder)
    {
        builder.ToTable(TableNames.UserRoleRelationships);
        
        builder.HasKey(ur => new { ur.RolesId, ur.UsersId });
        
        builder.HasOne<UserReadEntity>()
            .WithMany()
            .HasForeignKey(ug => ug.UsersId);

        builder.HasOne<RoleReadEntity>()
            .WithMany()
            .HasForeignKey(ug => ug.RolesId);
    }
}