using GameReviews.Domain.Results.Errors;

namespace GameReviews.Domain.Common.Errors;

internal static class UserErrors
{
    public static Error InvalidRefreshTokenError() => 
        new ("DomainError.User.InvalidRefreshToken","Invalid refresh token",ErrorType.Failure);
    
    public static Error RoleNotExist(string roleName) =>
        new ("DomainError.RoleNotExist",$"Role with name {roleName} does not exist", ErrorType.Failure);
}