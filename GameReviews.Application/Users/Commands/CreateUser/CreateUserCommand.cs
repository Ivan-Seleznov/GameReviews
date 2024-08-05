using GameReviews.Application.Common.Models.Dtos;
using MediatR;

namespace GameReviews.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string Username,
    string Email) : IRequest<UserDetailsDto>;