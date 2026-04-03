using MediatR;

namespace GameReviews.Application.Common.Behaviours;

public class PaginationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IPaginatedRequest
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        request.Page = Math.Clamp(request.Page, 1, int.MaxValue);
        request.PageSize = Math.Clamp(request.PageSize, 1, request.MaxPageSize);

        return await next();
    }
}