using AutoMapper;
using System.Reflection;
using GameReviews.Domain;
using GameReviews.Domain.Common.Abstractions.Entities;

namespace GameReviews.Application.Common.MappingProfiles.Ids;
internal class IdsMappingProfile : Profile
{
    private const string EntityIdPropertyName = "Value";
    public IdsMappingProfile()
    {
        //GOVNO. This setup is performed when the profile is first used
        ApplyIdMappingsFromAssembly();
    }

    private void ApplyIdMappingsFromAssembly()
    {
        //Define the assemblies where the entities and DTOs are located
        var baseEntityIdType = typeof(BaseEntityTypedId<>);
        var assembly = new Assembly[]
        {
            typeof(IGameReviewsApplicationMarker).Assembly,
            typeof(IGameReviewsDomainMarker).Assembly,
        };
        var allTypes = assembly.SelectMany(a => a.GetTypes());

        var entityIdTypes = allTypes.Where(t => IsSubclassOfRawGeneric(t, baseEntityIdType) && !t.IsAbstract);

        foreach (var entityIdType in entityIdTypes)
        {
            var genericArgument = entityIdType.BaseType!.GetGenericArguments().First();

            CreateMap(genericArgument, entityIdType)
                .ConvertUsing((src, ctx) =>
                {
                    return Activator.CreateInstance(entityIdType, new object[] { src });
                });

            CreateMap(entityIdType, genericArgument)
                .ConvertUsing(entityId => entityId.GetType().GetProperty(EntityIdPropertyName)!.GetValue(entityId));
        }
    }

    private bool IsSubclassOfRawGeneric(Type toCheck, Type generic)
    {
        while (toCheck is not null && toCheck != typeof(object))
        {
            var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
            if (cur == generic)
            {
                return true;
            }
            toCheck = toCheck.BaseType;
        }
        return false;
    }
}