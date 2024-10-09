using GameReviews.Domain.Common.Abstractions.Entities;

namespace GameReviews.Domain.Entities.Game;
public record GameId(long Value) : BaseEntityTypedId<long>(Value);