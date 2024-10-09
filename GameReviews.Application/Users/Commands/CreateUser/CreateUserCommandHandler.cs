using AutoMapper;
using GameReviews.Application.Common.Errors;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Authentication;
using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Interfaces.Repositories;
using GameReviews.Application.Common.Models.Dtos.User;
using GameReviews.Domain.Common.Result;
using GameReviews.Domain.DomainEvents.UserEvents;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Application.Users.Commands.CreateUser;

internal class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserDetailsDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRolesRepository _rolesRepository;

    public CreateUserCommandHandler(
        IUsersRepository usersRepository,
        IMapper mapper, IUnitOfWork unitOfWork,
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
        var user = _mapper.Map<UserEntity>(request);

        user.PasswordHash = _passwordHasher.Hash(request.Password);
        if (request.RoleName is not null)
        {
            var role = await _rolesRepository.GetByNameAsync(request.RoleName);
            if (role is null)
            {
                return AuthErrors.RoleNotExist(request.RoleName);
            }

            user.Roles.Add(role);
        }

        user.AddDomainEvent(new UserCreatedDomainEvent(user));
        var createdUser = await _usersRepository.AddAsync(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserDetailsDto>(createdUser);
    }
}