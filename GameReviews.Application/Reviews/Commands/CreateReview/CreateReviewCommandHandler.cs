using AutoMapper;
using GameReviews.Application.Common.Errors;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Interfaces.Repositories;
using GameReviews.Application.Common.Models.Dtos.Review;
using GameReviews.Application.Games.Queries.GetGame;
using GameReviews.Domain.Entities.Game;
using GameReviews.Domain.Entities.Review;
using GameReviews.Domain.Results;
using MediatR;
using MethodTimer;

namespace GameReviews.Application.Reviews.Commands.CreateReview;
public class CreateReviewCommandHandler : ICommandHandler<CreateReviewCommand,ReviewDetailsDto>
{
    private readonly IReviewsRepository _reviewsRepository;
    private readonly IGamesRepository _gamesRepository;
    private readonly IUsersRepository _usersRepository;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserIdStorage _userIdStorage;
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    public CreateReviewCommandHandler(
        IReviewsRepository reviewsRepository,
        IGamesRepository gamesRepository,
        IUsersRepository usersRepository,
        IUnitOfWork unitOfWork,
        IUserIdStorage userIdStorage,
        ISender sender,
        IMapper mapper)
    {
        _reviewsRepository = reviewsRepository;
        _gamesRepository = gamesRepository;
        _usersRepository = usersRepository;

        _unitOfWork = unitOfWork;
        _userIdStorage = userIdStorage;
        _sender = sender;
        _mapper = mapper;
    }

    [Time]
    public async Task<Result<ReviewDetailsDto>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = _userIdStorage.UserId!;

        if (!await _gamesRepository.ExistsAsync(request.GameId))
        {
            var gameResult = await _sender.Send(new GetGameQuery(request.GameId));
            if (gameResult.IsFailure)
            {
                return gameResult.Error;
            }

            var game = _mapper.Map<GameEntity>(gameResult.Value);
            await _usersRepository.CreateOrAddGameToUser(userId, game);
        }
        else
        {
            if (await _reviewsRepository.ExistsAsync(userId,request.GameId))
            {
                return ReviewErrors.AlreadyExists(userId, request.GameId);
            }
            if (!await _usersRepository.UserHasGameAsync(userId, request.GameId))
            {
                await _usersRepository.AddGameToUser(userId, request.GameId);
            }
        }

        var review = new ReviewEntity
        {
            Title = request.Title,
            Content = request.Content,
            Rating = request.Rating,
            AuthorId = userId,
            GameId = request.GameId 
        };

        await _reviewsRepository.AddAsync(review);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ReviewDetailsDto>(await _reviewsRepository.GetByIdWithRelatedEntitiesAsync(review.Id));
    }
}