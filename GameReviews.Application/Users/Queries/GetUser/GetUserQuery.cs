using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.User;
using GameReviews.Domain.Entities.User;
using MediatR;

namespace GameReviews.Application.Users.Queries.GetUser;

public record GetUserQuery(UserId UserId) : IQuery<UserDetailsDto>;
