using AutoMapper;
using GameReviews.Application.Common;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Authentication;
using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Interfaces.Repositories;
using GameReviews.Application.Common.Models.Dtos.Jwt;
using GameReviews.Application.Common.Models.Dtos.User;
using GameReviews.Domain.Entities.RefreshToken;
using GameReviews.Domain.Entities.User;
using GameReviews.Domain.Exceptions;

namespace GameReviews.Application.Users.Commands.LoginUser;

internal sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, AuthUserDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRefreshTokenProvider _refreshTokenProvider;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LoginUserCommandHandler(
        IUsersRepository usersRepository,
        IJwtProvider jwtProvider,
        IMapper mapper,
        IPasswordHasher passwordHasher,
        IRefreshTokenProvider refreshTokenProvider,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _jwtProvider = jwtProvider;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _refreshTokenProvider = refreshTokenProvider;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthUserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByUsernameAsync(request.Username);
        if (user == null)
        {
            throw new Exception("User does not exist");
        }

        var verified = _passwordHasher.Verify(request.Password, user.PasswordHash);
        if (!verified)
        {
            throw new Exception("User password is incorrect");
        }

        var jwtToken = _jwtProvider.GenerateToken(_mapper.Map<JwtTokenGenerateRequestDto>(user));
        var refreshToken = _refreshTokenProvider.GenerateToken(user.Id);

        _refreshTokenRepository.RemoveTokens(user.Id);
        await _refreshTokenRepository.AddAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthUserDto
        {
            AccessToken = jwtToken,
            RefreshToken = refreshToken.Token,
            User = _mapper.Map<UserDetailsDto>(user)
        };
    }
}