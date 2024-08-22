using AutoMapper;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Interfaces.Repositories;
using GameReviews.Application.Common.Models.Dtos.User;
using MediatR;

namespace GameReviews.Application.Users.Queries.GetUser;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery,UserDetailsDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    public async Task<UserDetailsDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(request.UserId);
        if (user is null)
        {
            //throw exception
        }

        return _mapper.Map<UserDetailsDto>(user);
    }
}

