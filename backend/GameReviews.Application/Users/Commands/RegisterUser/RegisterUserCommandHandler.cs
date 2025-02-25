using AutoMapper;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Models.Dtos.User;
using MediatR;
using GameReviews.Application.Common;
using GameReviews.Application.Common.Models.Dtos.Jwt;
using GameReviews.Application.Common.Interfaces.Authentication;
using GameReviews.Application.Users.Commands.CreateUserEntity;
using GameReviews.Domain.Common.Abstractions.Repositories;
using GameReviews.Domain.Common.Abstractions.Services;
using GameReviews.Domain.Entities.RolesAggregate.Entities;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Domain.Results;

namespace GameReviews.Application.Users.Commands.RegisterUser;
internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand,AuthUserDto>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRoleAssignmentService _roleAssignmentService;
    
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public RegisterUserCommandHandler(
        IJwtProvider jwtProvider, 
        IMapper mapper, 
        ISender sender,
        IPasswordHasher passwordHasher,
        IRefreshTokenGenerator refreshToken, 
        IUnitOfWork unitOfWork,
        IUsersRepository usersRepository,
        IUserRoleAssignmentService roleAssignmentService)
    {
        _jwtProvider = jwtProvider;
        _mapper = mapper;
        _sender = sender;
        _refreshTokenGenerator = refreshToken;
        _unitOfWork = unitOfWork;
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
        _roleAssignmentService = roleAssignmentService;
    }

    public async Task<Result<AuthUserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userCreationResult = await _sender.Send(
            new CreateUserEntityCommand(request.Username, request.Email, request.Password, Role.Registered.Name), 
            cancellationToken);
        
        if (userCreationResult.IsFailure)
        {
            return userCreationResult.Error;
        }

        var user = userCreationResult.Value;
        
        var jwtToken = _jwtProvider.GenerateToken(new JwtTokenGenerateRequestDto
        {
            Email = user.Email, Username = user.Username, Id = user.Id
        });

        var refreshToken = _refreshTokenGenerator.GenerateToken();
        user.AddRefreshToken(refreshToken.Token, refreshToken.ExpiresIn);
        
        return new AuthUserDto
        {
            User = _mapper.Map<UserDetailsDto>(user),
            AccessToken = jwtToken,
            RefreshToken = refreshToken.Token
        };
    }
}