using MediatR;

namespace GameReviews.Application.Common.Interfaces.Command;

public interface ICommand : IRequest;
public interface ICommand<out TResponse> : IRequest<TResponse>;