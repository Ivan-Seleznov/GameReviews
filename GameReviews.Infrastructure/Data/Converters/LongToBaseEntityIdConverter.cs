using GameReviews.Domain.Common.Abstractions.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameReviews.Infrastructure.Data.Converters;

internal class LongToBaseEntityIdConverter<T> : ValueConverter<T, long>
    where T : EntityId<long>
{
    public LongToBaseEntityIdConverter() : this(null)
    {
    }
    
    public LongToBaseEntityIdConverter(ConverterMappingHints mappingHints = null)
        : base(
            convertToProviderExpression: x => x.Value,
            convertFromProviderExpression: x => CreateInstance(x),
            mappingHints: mappingHints)
    {
    }
    
    private static T CreateInstance(long value)
    {
        var constructor = typeof(T).GetConstructor(new[] { typeof(long) });
        if (constructor == null)
        {
            throw new InvalidOperationException($"No suitable constructor found for type {typeof(T).FullName}");
        }
        return (T)constructor.Invoke(new object[] { value });
    }
}