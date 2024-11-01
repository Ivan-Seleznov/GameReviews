using System.Reflection;
using GameReviews.Domain;
using GameReviews.Domain.Common.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GameReviews.Infrastructure.Data.Extensions;
public record struct DiType(Type Interface, Type Type);
public static class ServiceDependencyInjectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var assemblies = new Assembly[]
        {
            typeof(IGameReviewsDomainMarker).Assembly,
            typeof(IGameReviewsInfrastructureMarker).Assembly
        };

        services.AddServicesFromAssemblies(
            assemblies,
            types => types
                .Where(t => !t.IsInterface && !t.IsAbstract)
                .SelectMany(GetRepositoryInterfaces, (t, i) => new DiType(i, t))
                .Distinct(),
            (diType, collection) => collection.AddScoped(diType.Interface, diType.Type)
        );

        return services;
    }

    private static IEnumerable<Type> GetRepositoryInterfaces(Type type)
    {
        var interfaces = type.GetInterfaces();

        return interfaces.Where(i =>
            i.IsInterface &&
            !i.IsGenericType &&
            (i.IsGenericType &&
             i.GetGenericTypeDefinition() ==
             typeof(IRepository<,>) ||
             i.GetInterfaces()
                 .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<,>))));
    }

    public static IServiceCollection AddServicesFromAssemblies(this IServiceCollection services, Assembly[] assemblies,
        Func<IEnumerable<Type>, IEnumerable<DiType>> selectTypesFunc, Action<DiType, IServiceCollection> registerTypeAction)
    {
        var types = assemblies.SelectMany(x => x.GetTypes());
        var diTypes = selectTypesFunc(types);

        foreach (var diType in diTypes)
        {
            registerTypeAction(diType, services);
        }

        return services;
    }
}