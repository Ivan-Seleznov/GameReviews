using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Common.Abstractions.Repositories;

namespace GameReviews.Domain.Entities.UserAggregate.Rules;

public class UsernameMustBeUniqueRule : IBusinessRule
{
    private readonly string _username;
    private readonly IUsersRepository _usersRepository;
    public UsernameMustBeUniqueRule(string username, IUsersRepository usersRepository)
    {
        _username = username;
        _usersRepository = usersRepository;
    }
    public string Message => $"Username: \"{_username}\" must be unique.";
    public async Task<bool> IsBrokenAsync()
    {
        return await _usersRepository.IsUsernameExistsAsync(_username);
    }
}