using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Domain.Entities.GameAggregate.Entities;

namespace GameReviews.Application.Users.Commands.AddGame;
public record AddGameToUserCommand(GameId GameId) : ICommand<GameInfoDto>;