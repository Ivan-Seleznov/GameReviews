using AutoMapper;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Models.Dtos.User;
using MediatR;
using GameReviews.Application.Common;
using GameReviews.Application.Common.Models.Dtos.Jwt;
using GameReviews.Application.Users.Commands.CreateUser;
using GameReviews.Domain.Entities.User;
using GameReviews.Application.Common.Interfaces.Repositories;
using GameReviews.Domain.Entities.Roles;
using GameReviews.Application.Common.Interfaces.Authentication;

namespace GameReviews.Application.Users.Commands.RegisterUser;
internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand,AuthUserDto>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IRefreshTokenProvider _refreshTokenProvider;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public RegisterUserCommandHandler(
        IJwtProvider jwtProvider, 
        IMapper mapper, 
        ISender sender,
        IPasswordHasher passwordHasher,
        IRefreshTokenProvider refreshToken, 
        IRefreshTokenRepository refreshTokenRepository, 
        IUnitOfWork unitOfWork)
    {
        _jwtProvider = jwtProvider;
        _mapper = mapper;
        _sender = sender;
        _refreshTokenProvider = refreshToken;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthUserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new CreateUserCommand(request.Username, request.Email, request.Password, Role.Registered.Name), cancellationToken);

        var userId = new UserId(result.Id);
        var jwtToken = _jwtProvider.GenerateToken(new JwtTokenGenerateRequestDto
        {
            Email = result.Email, Username = result.Username, Id = userId
        });

        var refreshToken = _refreshTokenProvider.GenerateToken(userId);
        await _refreshTokenRepository.AddAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthUserDto
        {
            User = result,
            AccessToken = jwtToken,
            RefreshToken = refreshToken.Token
        };
    }
}