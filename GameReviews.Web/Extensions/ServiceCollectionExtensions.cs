﻿using GameReviews.Application.Users.Repository;
using GameReviews.Web.ExceptionHandlers;

namespace GameReviews.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddExceptionHandler<ValidationExceptionHandler>();

        return serviceCollection;
    }
}
