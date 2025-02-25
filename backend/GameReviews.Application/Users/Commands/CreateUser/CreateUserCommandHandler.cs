using AutoMapper;
using GameReviews.Application.Common.Interfaces.Command;
using GameReviews.Application.Common.Models.Dtos.User;
using GameReviews.Application.Users.Commands.CreateUserEntity;
using GameReviews.Domain.Results;
using MediatR;

namespace GameReviews.Application.Users.Commands.CreateUser;

internal class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserDetailsDto>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    public CreateUserCommandHandler(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }
    public async Task<Result<UserDetailsDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var creationResult = await _sender.Send(new CreateUserEntityCommand(request.Username, request.Email, request.Password), cancellationToken);
        if (creationResult.IsFailure)
        {
            return creationResult.Error;
        }
        
        return _mapper.Map<UserDetailsDto>(creationResult.Value);
    }
}