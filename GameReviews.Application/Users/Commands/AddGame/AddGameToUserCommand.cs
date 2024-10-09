using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Domain.Entities.Game;

namespace GameReviews.Application.Users.Commands.AddGame;
public record AddGameToUserCommand(GameId GameId) : ICommand<GameDetailsDto>;