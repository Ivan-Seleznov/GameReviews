using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Common.Abstractions.Repositories;
using GameReviews.Domain.Common.Errors;
using GameReviews.Domain.Common.Rules;
using GameReviews.Domain.DomainEvents.UserEvents;
using GameReviews.Domain.Entities.UserAggregate.Rules;
using GameReviews.Domain.Results;

namespace GameReviews.Domain.Entities.UserAggregate.Entities;

public class UserEntity : BaseEntity<UserId>, IAggregateRoot
{
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public IReadOnlyCollection<RefreshTokenEntity> RefreshTokens => _refreshTokens.AsReadOnly();
    private List<RefreshTokenEntity> _refreshTokens = new();

    private UserEntity() {}

    private UserEntity(string username, string email, string passwordHash)
    {
        Username = username;
        PasswordHash = passwordHash;
        Email = email;
    }

    public static async Task<Result<UserEntity>> CreateAsync(
        string username, 
        string email, 
        string passwordHash,
        IUsersRepository usersRepository)
    {
        var result = await BusinessRuleValidator.CheckRulesAsync(
            new EmailMustBeUniqueRule(email, usersRepository), 
            new UsernameMustBeUniqueRule(username, usersRepository));

        if (result.IsFailure)
        {
            return result.Error;
        }
        
        var user = new UserEntity(username, email, passwordHash);
        user.AddDomainEvent(new UserCreatedDomainEvent(user));
        return user;
    }

    public void AddRefreshToken(string token, DateTime expiresIn)
    {
        var refreshTokenEntity = new RefreshTokenEntity(token, expiresIn);
        _refreshTokens.Add(refreshTokenEntity);
    }
    
    public Result UpdateRefreshToken(string oldToken, string newToken, DateTime expiresIn)
    {
        var refreshToken = _refreshTokens.SingleOrDefault(t => t.Token == oldToken);
        if (refreshToken is null || !refreshToken.IsActive)
        {
            return UserErrors.InvalidRefreshTokenError();
        }
        
        _refreshTokens.Remove(refreshToken);
        AddRefreshToken(newToken, expiresIn);
        
        return Result.Success();
    }

    public void RemoveExpiredTokens(bool onlyInactive = true)
    {
        _refreshTokens.RemoveAll(token => !token.IsActive);
    }

    public void RemoveAllTokens()
    {
        _refreshTokens.Clear();
    }
}
public record UserId(int Value) : EntityId<int>(Value);