using System.Reflection;
using GameReviews.Domain.Common.Abstractions.Entities;

namespace GameReviews.Domain.Common;

public abstract class Enumeration<TEnum> : BaseEntity<int>, IEquatable<TEnum>
    where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<int, TEnum> Enumerations = CreateEnumeration();

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public string Name { get; protected init; } = string.Empty;

    public static TEnum? FromValue(int value)
    {
        return Enumerations.TryGetValue(
            value, 
            out TEnum? enumeration) ? enumeration : null;
    }

    public static TEnum? FromName(string name)
    {
        return Enumerations.Values.SingleOrDefault(e => e.Name == name);
    }

    public static IReadOnlyCollection<TEnum> GetValues()
    {
        return Enumerations.Values;
    }
    private static Dictionary<int, TEnum> CreateEnumeration()
    {
        var enumerationType = typeof(TEnum);
        var fields = enumerationType
            .GetFields(BindingFlags.Public | 
                       BindingFlags.Static |
                       BindingFlags.FlattenHierarchy)
            .Where(fieldInfo => 
                enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => 
                (TEnum)fieldInfo.GetValue(null)!);

        return fields.ToDictionary(f => f.Id);
    }

    public bool Equals(TEnum? other)
    {
        return other is not null && 
               GetType() == other.GetType() &&
               Id == other.Id;
    }
    public override bool Equals(object? obj)
    {
        return obj is Enumeration<TEnum> other && 
               Equals(other);
    }
}
