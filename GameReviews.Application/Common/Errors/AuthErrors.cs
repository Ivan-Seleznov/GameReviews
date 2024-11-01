using GameReviews.Domain.Results.Errors;

namespace GameReviews.Application.Common.Errors;
public static class AuthErrors
{
    public static Error Authentication()
        => new("Error.Authentication", "Authentication error", ErrorType.Failure);

    public static Error InvalidCredentials()
        => new("Error.InvalidCredentials", "Invalid login credentials", ErrorType.Failure);
}