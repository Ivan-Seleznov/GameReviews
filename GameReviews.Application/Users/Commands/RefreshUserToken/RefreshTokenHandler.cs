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

namespace GameReviews.Application.Users.Commands.RefreshUserToken;

internal sealed class RefreshTokenHandler : ICommandHandler<RefreshUserTokenCommand, AuthUserDto>
{
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public RefreshTokenHandler(
        IRefreshTokenGenerator refreshTokenGenerator, 
        IJwtProvider jwtProvider, 
        IUsersRepository usersRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _refreshTokenGenerator = refreshTokenGenerator;
        _jwtProvider = jwtProvider;
        _usersRepository = usersRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<AuthUserDto>> Handle(RefreshUserTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = _jwtProvider.GetUserIdFromToken(request.AccessToken);
        var user = await _usersRepository.GetByIdAsync(userId);

        if (user is null)
        {
            return AuthErrors.Authentication();
        }

        var newRefreshToken = _refreshTokenGenerator.GenerateToken();
        var updateTokenResult = user.UpdateRefreshToken(request.RefreshToken, newRefreshToken.Token, newRefreshToken.ExpiresIn);
        if (!updateTokenResult.IsFailure)
        {
            return AuthErrors.Authentication();
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var jwtToken = _jwtProvider.GenerateToken(_mapper.Map<JwtTokenGenerateRequestDto>(user));
        return new AuthUserDto()
        {
            AccessToken = jwtToken,
            RefreshToken = newRefreshToken.Token,
            User = _mapper.Map<UserDetailsDto>(user)
        };
    }
}