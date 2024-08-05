using GameReviews.Application.Common.Models.Dtos;
using GameReviews.Domain.Entities.User;
using MediatR;

namespace GameReviews.Application.Users.Queries.GetUser;

public record GetUserQuery(UserId UserId) : IRequest<UserDetailsDto>;
