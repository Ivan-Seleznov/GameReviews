using AutoMapper;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Models.Dtos;
using GameReviews.Application.Users.Repository;
using GameReviews.Domain.DomainEvents.UserEvents;
using GameReviews.Domain.Entities.User;
using MediatR;

namespace GameReviews.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDetailsDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public CreateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDetailsDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<UserEntity>(request);
        user.AddDomainEvent(new UserCreatedDomainEvent(user));

        var createdUser = await _usersRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserDetailsDto>(createdUser);
    }
}