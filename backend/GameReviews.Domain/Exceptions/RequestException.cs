using System.Net;

namespace GameReviews.Domain.Exceptions;

public class RequestException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public RequestException(HttpStatusCode statusCode, string? message) : base(message)
    {
        StatusCode = statusCode;
    }
}

