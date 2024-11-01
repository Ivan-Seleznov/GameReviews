using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameReviews.Application.Common.Errors;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Common.Models.Dtos.Review;
using GameReviews.Application.Common.Models.Dtos.User;
using GameReviews.Application.Games.Queries.GetGame;
using GameReviews.Domain.Common.Abstractions.Repositories;
using GameReviews.Domain.Common.Abstractions.Services;
using GameReviews.Domain.Entities.GameAggregate.Entities;
using GameReviews.Domain.Entities.ReviewAggregate.Entities;
using GameReviews.Domain.Results;
using MediatR;
using MethodTimer;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Application.Reviews.Commands.CreateReview;
public class CreateReviewCommandHandler : ICommandHandler<CreateReviewCommand,ReviewDetailsDto>
{
    private readonly IReviewsRepository _reviewsRepository;
    private readonly IGamesRepository _gamesRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IGameUserRelationshipService _gameUserRelationshipService;
    private readonly IReadApplicationDbContext _readApplicationDbContext;
    
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
        IMapper mapper,
        IGameUserRelationshipService gameUserRelationshipService,
        IReadApplicationDbContext readApplicationDbContext)
    {
        _reviewsRepository = reviewsRepository;
        _gamesRepository = gamesRepository;
        _usersRepository = usersRepository;

        _unitOfWork = unitOfWork;
        _userIdStorage = userIdStorage;
        _sender = sender;
        _mapper = mapper;
        _gameUserRelationshipService = gameUserRelationshipService;
        _readApplicationDbContext = readApplicationDbContext;
    }

    [Time]
    public async Task<Result<ReviewDetailsDto>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = _userIdStorage.UserId!;
 
        if (!await _gamesRepository.ExistsAsync(request.GameId))
        {
            var gameResult = await _sender.Send(new GetGameQuery(request.GameId), cancellationToken);
            if (gameResult.IsFailure)
            {
                return gameResult.Error;
            }
            
            var game = GameEntity.Create(
                request.GameId, 
                gameResult.Value.Name, 
                gameResult.Value.Description);
            
            await _gamesRepository.AddAsync(game);
            await _gameUserRelationshipService.CreateRelationshipAsync(userId, request.GameId);
        }
        else
        {
            if (await _reviewsRepository.ExistsAsync(userId,request.GameId))
            {
                return ReviewErrors.AlreadyExists(userId, request.GameId);
            }
            if (!await _usersRepository.UserHasGameAsync(userId, request.GameId))
            {
                await _gameUserRelationshipService.CreateRelationshipAsync(userId, request.GameId);
            }
        }

        var review = ReviewEntity.Create(
            request.Title,
            request.Content,
            request.Rating,
            userId,
            request.GameId);
        
        if (review.IsFailure)
        {
            return review.Error;
        }
        
        await _reviewsRepository.AddAsync(review.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var authorDto = await _readApplicationDbContext.Users.Where(u => u.Id == review.Value.AuthorId).ProjectTo<UserInfoDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        var gameDto = await _readApplicationDbContext.Games.Where(g => g.Id == review.Value.GameId).ProjectTo<GameInfoDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return new ReviewDetailsDto
        {
            Id = review.Value.Id.Value,
            Author = authorDto!,
            Game = gameDto!,
            Content = review.Value.Content,
            CreatedAt = review.Value.CreatedAt,
            Rating = review.Value.Rating,
            Title = review.Value.Title,
        };
    }
}