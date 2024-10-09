﻿using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Domain.Entities.Game;

namespace GameReviews.Application.Games.Queries.GetGame;
public record GetGameQuery(GameId Id) : IQuery<GameDetailsDto>;