using GameReviews.Domain.Common.Abstractions.Repositories;
using GameReviews.Domain.Common.Abstractions.Services;
using GameReviews.Domain.Common.Errors;
using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Domain.Entities.UserGameRelationAggregate.Entities;
using GameReviews.Domain.Results;

namespace GameReviews.Domain.Entities.UserGameRelationAggregate.Services;
internal sealed class GameUserRelationshipService : IGameUserRelationshipService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IGamesRepository _gamesRepository;

    public GameUserRelationshipService(IUsersRepository usersRepository, IGamesRepository gamesRepository)
    {
        _usersRepository = usersRepository;
        _gamesRepository = gamesRepository;
    }

    public async Task<Result> CreateRelationshipAsync(UserId userId, GameId gameId)
    {
        if (!await _usersRepository.ExistsAsync(userId))
        {
            return Result.Failure(UserGameRelationshipErrors.UserNotExist(userId));
        }

        if (!await _gamesRepository.ExistsAsync(gameId))
        {
            return Result.Failure(UserGameRelationshipErrors.GameNotExist(gameId));
        }
        
        await _usersRepository.AddUserGameRelationAsync(new GameUserRelationship(gameId, userId));
        return Result.Success();
    }
}