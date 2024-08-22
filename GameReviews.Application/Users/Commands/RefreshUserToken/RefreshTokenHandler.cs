using AutoMapper;
using GameReviews.Application.Common;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Authentication;
using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Interfaces.Repositories;
using GameReviews.Application.Common.Models.Dtos.Jwt;
using GameReviews.Application.Common.Models.Dtos.User;

namespace GameReviews.Application.Users.Commands.RefreshUserToken;

internal sealed class RefreshTokenHandler : ICommandHandler<RefreshUserTokenCommand, AuthUserDto>
{
    private readonly IRefreshTokenProvider _refreshTokenProvider;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public RefreshTokenHandler(IRefreshTokenProvider refreshTokenProvider, IJwtProvider jwtProvider,
        IRefreshTokenRepository refreshTokenRepository, IUsersRepository usersRepository, IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _refreshTokenProvider = refreshTokenProvider;
        _jwtProvider = jwtProvider;
        _refreshTokenRepository = refreshTokenRepository;
        _usersRepository = usersRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthUserDto> Handle(RefreshUserTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = _jwtProvider.GetUserIdFromToken(request.AccessToken);
        var user = await _usersRepository.GetByIdAsync(userId);

        if (user is null)
        {
            throw new Exception("Authorization");
        }

        var refreshToken = await _refreshTokenRepository.GetTokenByUserIdAsync(request.RefreshToken, user.Id);
        if (refreshToken is null || !refreshToken.IsActive)
        {
            throw new Exception("Authorization");
        }
        _refreshTokenRepository.Remove(refreshToken);

        var newRefreshToken = _refreshTokenProvider.GenerateToken(userId);
        var jwtToken = _jwtProvider.GenerateToken(_mapper.Map<JwtTokenGenerateRequestDto>(user));

        await _refreshTokenRepository.AddAsync(newRefreshToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthUserDto()
        {
            AccessToken = jwtToken,
            RefreshToken = newRefreshToken.Token,
            User = _mapper.Map<UserDetailsDto>(user)
        };
    }
}