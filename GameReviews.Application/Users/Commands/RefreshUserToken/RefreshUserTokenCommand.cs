using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Models.Dtos.User;

namespace GameReviews.Application.Users.Commands.RefreshUserToken;

public record RefreshUserTokenCommand(
    string AccessToken,
    string RefreshToken) : ICommand<AuthUserDto>;