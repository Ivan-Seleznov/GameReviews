using FluentValidation;
using GameReviews.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GameReviews.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(IGameReviewsApplicationMarker).Assembly;

            services.AddMediatR(conf => 
            {
                conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                conf.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });

            services.AddValidatorsFromAssembly(assembly,includeInternalTypes:true);

            services.AddAutoMapper(assembly);
            return services;
        }
    }
}
