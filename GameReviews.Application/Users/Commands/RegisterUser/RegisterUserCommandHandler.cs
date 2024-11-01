using AutoMapper;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Models.Dtos.User;
using MediatR;
using GameReviews.Application.Common;
using GameReviews.Application.Common.Models.Dtos.Jwt;
using GameReviews.Application.Common.Interfaces.Authentication;
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
        var userResult = await UserEntity.CreateAsync(
            request.Username, 
            request.Email, 
            _passwordHasher.Hash(request.Password),
            _usersRepository);
        
        if (userResult.IsFailure)
        {
            return userResult.Error;
        }
        
        var user = userResult.Value;
        await _usersRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var assignmentResult  = await _roleAssignmentService.AssignRoleToUserAsync(user.Id, Role.Registered.Name);
        if (assignmentResult.IsFailure)
        {
            return assignmentResult.Error;
        }
        
        var jwtToken = _jwtProvider.GenerateToken(new JwtTokenGenerateRequestDto
        {
            Email = user.Email, Username = user.Username, Id = user.Id
        });

        var refreshToken = _refreshTokenGenerator.GenerateToken();
        user.AddRefreshToken(refreshToken.Token, refreshToken.ExpiresIn);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthUserDto
        {
            User = _mapper.Map<UserDetailsDto>(user),
            AccessToken = jwtToken,
            RefreshToken = refreshToken.Token
        };
    }
}