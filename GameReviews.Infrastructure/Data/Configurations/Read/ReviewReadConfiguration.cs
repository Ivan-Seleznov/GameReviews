﻿using GameReviews.Application.Common.Models.ReadEntities;
using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameReviews.Infrastructure.Data.Configurations.Read;
internal class ReviewReadConfiguration : IEntityTypeConfiguration<ReviewReadEntity>
{
    public void Configure(EntityTypeBuilder<ReviewReadEntity> builder)
    {
        builder.ToTable(TableNames.Reviews);

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.AuthorId, x.GameId })
            .IsUnique();

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Rating)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Content)
            .HasMaxLength(1000);

        builder.Property(x => x.GameId)
            .HasConversion(x => x.Value, v => new GameId(v));
        builder.Property(x => x.AuthorId)
            .HasConversion(x => x.Value, v => new UserId(v));
    }
}