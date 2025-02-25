using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Authentication;
using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Domain.Common.Abstractions.Repositories;
using GameReviews.Domain.Common.Abstractions.Services;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Domain.Results;

namespace GameReviews.Application.Users.Commands.CreateUserEntity;

internal class CreateUserEntityCommandHandler : ICommandHandler<CreateUserEntityCommand, UserEntity>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    
    private readonly IUserRoleAssignmentService _userRoleAssignmentService;
    
    public CreateUserEntityCommandHandler(
        IUsersRepository usersRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IUserRoleAssignmentService userRoleAssignmentService)
    {
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _userRoleAssignmentService = userRoleAssignmentService;
    }

    public async Task<Result<UserEntity>> Handle(CreateUserEntityCommand request, CancellationToken cancellationToken)
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
        
        var user = await _usersRepository.AddAsync( userResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (request.RoleName is not null)
        {
            var roleAssignmentResult = await _userRoleAssignmentService.AssignRoleToUserAsync(user.Id, request.RoleName);
            if (roleAssignmentResult.IsFailure)
            {
                return roleAssignmentResult.Error;
            }
        }
        
        return user;
    }
}