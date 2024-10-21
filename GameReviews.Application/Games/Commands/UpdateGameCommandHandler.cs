using AutoMapper;
using GameReviews.Application.Common.Errors;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Interfaces.Repositories;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Domain.Results;

namespace GameReviews.Application.Games.Commands;
internal class UpdateGameCommandHandler : ICommandHandler<UpdateGameCommand,GameDetailsDto>
{
    private readonly IGamesRepository _gamesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UpdateGameCommandHandler(IGamesRepository gamesRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _gamesRepository = gamesRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<GameDetailsDto>> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _gamesRepository.GetByIdAsync(request.GameId);
        if (game is null)
        {
            return GameErrors.NotFound(request.GameId);
        }

        game.Name = request.Name;
        game.Description = request.Description;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<GameDetailsDto>(game);
    }
}