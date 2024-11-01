using GameReviews.Domain.Entities.RolesAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Domain.Entities.UserRoleAggregate.Entities;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations.Write;

internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleRelationshipAggregate>
{
    public void Configure(EntityTypeBuilder<UserRoleRelationshipAggregate> builder)
    {
        builder.ToTable(TableNames.UserRoleRelationships);
        
        builder.HasKey(ur => new { ur.RolesId, ur.UsersId });
        
        builder.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(ug => ug.UsersId);

        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(ug => ug.RolesId);
    }
}