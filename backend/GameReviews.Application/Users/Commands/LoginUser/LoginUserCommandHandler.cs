using AutoMapper;
using GameReviews.Application.Common;
using GameReviews.Application.Common.Errors;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Authentication;
using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Models.Dtos.Jwt;
using GameReviews.Application.Common.Models.Dtos.User;
using GameReviews.Domain.Common.Abstractions.Repositories;
using GameReviews.Domain.Results;

namespace GameReviews.Application.Users.Commands.LoginUser;

internal sealed class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, AuthUserDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator; private readonly IUnitOfWork _unitOfWork;

    public LoginUserCommandHandler(
        IUsersRepository usersRepository,
        IJwtProvider jwtProvider,
        IMapper mapper,
        IPasswordHasher passwordHasher,
        IRefreshTokenGenerator refreshTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _jwtProvider = jwtProvider;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _refreshTokenGenerator = refreshTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AuthUserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByUsernameAsync(request.Username);
        if (user == null)
        {
            return AuthErrors.InvalidCredentials();
        }

        var verified = _passwordHasher.Verify(request.Password, user.PasswordHash);
        if (!verified)
        {
            return AuthErrors.InvalidCredentials();
        }

        var jwtToken = _jwtProvider.GenerateToken(_mapper.Map<JwtTokenGenerateRequestDto>(user));
        var refreshToken = _refreshTokenGenerator.GenerateToken();
        
        user.AddRefreshToken(refreshToken.Token, refreshToken.ExpiresIn);
        user.RemoveExpiredTokens();
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthUserDto
        {
            AccessToken = jwtToken,
            RefreshToken = refreshToken.Token,
            User = _mapper.Map<UserDetailsDto>(user)
        };
    }
}