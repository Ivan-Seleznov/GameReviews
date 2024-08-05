using MediatR;

namespace GameReviews.Application.Common.Interfaces.Query;

internal interface IQueryHandler<in TRequest,TResponse> : IRequestHandler<TRequest,TResponse> 
    where TRequest : IQuery<TResponse>;