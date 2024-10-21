using GameReviews.Domain.Results;
using MediatR;

namespace GameReviews.Application.Common.Interfaces.Command;
public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest,Result> 
    where TRequest : ICommand;
public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest,Result<TResponse>>
    where TRequest : ICommand<TResponse>;