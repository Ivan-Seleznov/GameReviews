using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Domain.Entities.GameAggregate.Entities;

namespace GameReviews.Application.Games.Commands;
public record UpdateGameCommand(
    GameId GameId,
    string Name,
    string Description) : ICommand<GameDetailsDto>;