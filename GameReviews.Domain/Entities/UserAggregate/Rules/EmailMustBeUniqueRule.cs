using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Common.Abstractions.Repositories;

namespace GameReviews.Domain.Entities.UserAggregate.Rules;

internal class EmailMustBeUniqueRule : IBusinessRule
{
    private readonly string _email;
    private readonly IUsersRepository _usersRepository;

    public string Message => $"The email address: \"{_email}\" must be unique.";

    public EmailMustBeUniqueRule(string email, IUsersRepository usersRepository)
    {
        _email = email;
        _usersRepository = usersRepository;
        
    }
    
    public async Task<bool> IsBrokenAsync()
    {
        var result = await _usersRepository.IsEmailExistsAsync(_email);
        return result;
    }
}