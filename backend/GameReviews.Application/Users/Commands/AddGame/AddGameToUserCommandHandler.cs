using GameReviews.Application.Common.Interfaces.Command;
using AutoMapper;
using GameReviews.Application.Common.Errors;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Games.Queries.GetGame;
using GameReviews.Domain.Common.Abstractions.Repositories;
using GameReviews.Domain.Common.Abstractions.Services;
using GameReviews.Domain.Entities.GameAggregate.Entities;
using MediatR;
using GameReviews.Domain.Results;

namespace GameReviews.Application.Users.Commands.AddGame;
internal class AddGameToUserCommandHandler : ICommandHandler<AddGameToUserCommand,GameInfoDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUserIdStorage _userIdStorage;
    private readonly IGamesRepository _gamesRepository;
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGameUserRelationshipService _gameUserRelationshipService;
    public AddGameToUserCommandHandler(
        IUsersRepository usersRepository,
        IGamesRepository gamesRepository,
        IUserIdStorage userIdStorage,
        ISender sender,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IGameUserRelationshipService gameUserRelationshipService)
    {
        _usersRepository = usersRepository;
        _gamesRepository = gamesRepository;
        _userIdStorage = userIdStorage;

        _sender = sender;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _gameUserRelationshipService = gameUserRelationshipService;
    }
    public async Task<Result<GameInfoDto>> Handle(AddGameToUserCommand request, CancellationToken cancellationToken)
    {
        var userId = _userIdStorage.UserId!;

        GameEntity game;
        if (await _gamesRepository.ExistsAsync(request.GameId))
        {
            if (await _usersRepository.UserHasGameAsync(userId, request.GameId))
            {
                return UserErrors.AlreadyHasGame(userId, request.GameId);
            }

            game = (await _gamesRepository.GetByIdAsync(request.GameId))!;
        }
        else
        {
            var gameResult = await _sender.Send(new GetGameQuery(request.GameId));
            if (gameResult.IsFailure)
            {
                return gameResult.Error;
            }
            
            game = GameEntity.Create(
                new GameId(gameResult.Value.Id),
                gameResult.Value.Name, 
                gameResult.Value.Description);
            
            await _gamesRepository.AddAsync(game);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        var relationshipCreationResult = await _gameUserRelationshipService.CreateRelationshipAsync(userId,game.Id);
        if (relationshipCreationResult.IsFailure)
        {
            return relationshipCreationResult.Error;
        }
        
        return _mapper.Map<GameInfoDto>(game);
    }
}