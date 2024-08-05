using MediatR;

namespace GameReviews.Application.Common.Interfaces.Query;

public interface IQuery<out TResponse> : IRequest<TResponse>;

