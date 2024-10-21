using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Interfaces.Repositories;
using AutoMapper;
using GameReviews.Application.Common.Errors;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Games.Queries.GetGame;
using GameReviews.Domain.Entities.Game;
using MediatR;
using GameReviews.Domain.Results;

namespace GameReviews.Application.Users.Commands.AddGame;
internal class AddGameToUserCommandHandler : ICommandHandler<AddGameToUserCommand,GameDetailsDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUserIdStorage _userIdStorage;
    private readonly IGamesRepository _gamesRepository;
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public AddGameToUserCommandHandler(
        IUsersRepository usersRepository,
        IGamesRepository gamesRepository,
        IUserIdStorage userIdStorage,
        ISender sender,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _gamesRepository = gamesRepository;
        _userIdStorage = userIdStorage;

        _sender = sender;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<GameDetailsDto>> Handle(AddGameToUserCommand request, CancellationToken cancellationToken)
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

            game = _mapper.Map<GameEntity>(gameResult.Value);
        }

        await _usersRepository.CreateOrAddGameToUser(userId,game);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<GameDetailsDto>(game);
    }
}