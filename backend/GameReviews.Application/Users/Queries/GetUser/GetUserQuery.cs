using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.User;
using GameReviews.Domain.Entities.UserAggregate;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using MediatR;

namespace GameReviews.Application.Users.Queries.GetUser;

public record GetUserQuery(UserId UserId) : IQuery<UserDetailsDto>;
