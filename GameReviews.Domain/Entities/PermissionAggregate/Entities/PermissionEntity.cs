using GameReviews.Domain.Common.Abstractions.Entities;

namespace GameReviews.Domain.Entities.PermissionAggregate.Entities;
public class PermissionEntity : IAggregateRoot
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    
    private PermissionEntity() { }
    private PermissionEntity(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static PermissionEntity Create(int id, string name)
    {
        return new PermissionEntity(id, name);
    }
}