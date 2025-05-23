﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameReviews.Infrastructure.Data.Extensions;
internal static class ModelBuilderExtensions
{
    /// <summary>
    /// Applies a value converter to properties of a specified base type in the model.
    /// </summary>
    /// <param name="modelBuilder">The model builder to configure.</param>
    /// <param name="baseType">The base type of properties to apply the converter to.</param>
    /// <param name="converterBaseType">The base type of the value converter.</param>
    public static ModelBuilder ApplyValueConverters(
        this ModelBuilder modelBuilder,
        Type baseType,
        Type converterBaseType, 
        Action<PropertyBuilder>? propertyBuilderFunc = null)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.HasSharedClrType)
            {
                continue;
            }

            var properties = entityType.GetProperties()
                .Where(p => baseType.IsAssignableFrom(p.ClrType));

            foreach (var property in properties)
            {
                var concreteType = property.ClrType;
                var converterType = converterBaseType.MakeGenericType(concreteType);
                var converter = (ValueConverter)Activator.CreateInstance(converterType)!;

                var entity = modelBuilder.Entity(entityType.ClrType);
                var prop = entity.Property(property.Name)
                    .HasConversion(converter);

                propertyBuilderFunc?.Invoke(prop);
            }
        }

        return modelBuilder;
    }
}