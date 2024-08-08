using System.Net;
using System.Text.Json;
using GameReviews.Application.Common.Models;
using GameReviews.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
namespace GameReviews.Web.ExceptionHandlers;

public abstract class BaseExceptionHandler : IExceptionHandler
{
    public abstract ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
        Exception exception, CancellationToken cancellationToken);

    protected virtual async Task HandleExceptionAsync(HttpContext httpContext, Exception exception, IDictionary<string, string[]>? errors = null)
    {
        var statusCode = exception is RequestException requestException
            ? requestException.StatusCode
            : HttpStatusCode.InternalServerError;

        var message = statusCode is HttpStatusCode.InternalServerError 
            ? "Something went wrong" : exception.Message;

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)statusCode;

        await httpContext.Response.WriteAsync(SerializeErrorDetails(message, errors));
    }
    private protected string SerializeErrorDetails(string message, IDictionary<string, string[]>? errors = null)
    {
        var details = new ErrorDetails(message, errors);

        return JsonSerializer.Serialize(details);
    }
}

