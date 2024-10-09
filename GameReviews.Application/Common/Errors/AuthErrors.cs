using GameReviews.Domain.Common.Result.Errors;

namespace GameReviews.Application.Common.Errors;
public static class AuthErrors
{
    public static Error RoleNotExist(string roleName) 
        => new("Error.RoleNotExist", $"Role with name {roleName} does not exist",ErrorType.NotFound);
    public static Error Authentication()
        => new("Error.Authentication", "Authentication error", ErrorType.Failure);

    public static Error InvalidCredentials()
        => new("Error.InvalidCredentials", "Invalid login credentials", ErrorType.Failure);
}