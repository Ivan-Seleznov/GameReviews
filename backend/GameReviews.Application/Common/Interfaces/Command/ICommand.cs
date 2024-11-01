using GameReviews.Domain.Results;
using MediatR;

namespace GameReviews.Application.Common.Interfaces.Command;
public interface ICommand : IRequest<Result>;
public interface ICommand<TResponse> : IRequest<Result<TResponse>>;