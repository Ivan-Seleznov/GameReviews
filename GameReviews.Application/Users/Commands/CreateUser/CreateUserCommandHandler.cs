using AutoMapper;
using GameReviews.Application.Common.Errors;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Authentication;
using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Models.Dtos.User;
using GameReviews.Domain.Common.Abstractions.Repositories;
using GameReviews.Domain.Common.Abstractions.Services;
using GameReviews.Domain.DomainEvents.UserEvents;
using GameReviews.Domain.Entities.UserAggregate;
using GameReviews.Domain.Entities.UserAggregate.Entities;
using GameReviews.Domain.Results;

namespace GameReviews.Application.Users.Commands.CreateUser;

internal class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserDetailsDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRolesRepository _rolesRepository;
    private readonly IUserRoleAssignmentService _userRoleAssignmentService;
    public CreateUserCommandHandler(
        IUsersRepository usersRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IRolesRepository rolesRepository)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _rolesRepository = rolesRepository;
    }

    public async Task<Result<UserDetailsDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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
        if (request.RoleName is not null)
        {
            var roleAssignmentResult = await _userRoleAssignmentService.AssignRoleToUserAsync(user.Id, request.RoleName);
            if (roleAssignmentResult.IsFailure)
            {
                return roleAssignmentResult.Error;
            }
        }

        var createdUser = await _usersRepository.AddAsync(user);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UserDetailsDto>(createdUser);
    }
}