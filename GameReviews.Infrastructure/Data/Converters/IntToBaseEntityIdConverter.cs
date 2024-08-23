using GameReviews.Domain.Common.Abstractions.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameReviews.Infrastructure.Data.Converters;
internal class IntToBaseEntityIdConverter<T> : ValueConverter<T, int>
    where T : BaseEntityTypedId<int>
{
    public IntToBaseEntityIdConverter() : this(null)
    {
    }

    public IntToBaseEntityIdConverter(ConverterMappingHints mappingHints = null)
        : base(
            convertToProviderExpression: x => x.Value,
            convertFromProviderExpression: x => CreateInstance(x),
            mappingHints: mappingHints)
    {
    }

    private static T CreateInstance(int value)
    {
        var constructor = typeof(T).GetConstructor(new[] { typeof(int) });
        if (constructor == null)
        {
            throw new InvalidOperationException($"No suitable constructor found for type {typeof(T).FullName}");
        }
        return (T)constructor.Invoke(new object[] { value });
    }
}