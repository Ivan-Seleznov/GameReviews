using System.Data;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Domain.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Application.Common.Behaviours;

internal class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionBehaviour(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response = default!;
        if (_unitOfWork.GetCurrentTransaction() is not null) 
        {
            return await next();
        }

        var strategy = _unitOfWork.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction =
                await _unitOfWork.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

            try
            {
                response = await next();
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            
            if (response.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            await transaction.CommitAsync(cancellationToken);
        });

        return response;
    }
}