﻿using GameReviews.Domain.Common.Result;
using GameReviews.Web.Common.Results;

namespace GameReviews.Web.Extensions;

public static class ResultExtensions
{
    public static TOut Matches<TOut>(this Result result, Func<TOut> onSuccess, Func<Result, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result);
    }

    public static TOut Matches<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> onSuccess,
        Func<Result<TIn>, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
    }

    public static IResult WithProblemDetails(this Result result, Func<IResult> onSuccessResult) =>
        Matches(result, onSuccessResult, ApiResults.CreateProblemDetails);
    public static IResult WithProblemDetails<TIn>(this Result<TIn> result, Func<TIn, IResult> onSuccessResult) =>
        Matches(result, onSuccessResult, ApiResults.CreateProblemDetails);
}