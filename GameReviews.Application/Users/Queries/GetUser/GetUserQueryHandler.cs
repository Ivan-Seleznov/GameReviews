using AutoMapper;
using GameReviews.Application.Common.Errors;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Interfaces.Repositories;
using GameReviews.Application.Common.Models.Dtos.User;
using GameReviews.Domain.Common.Result;

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
    public async Task<Result<UserDetailsDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(request.UserId);
        if (user is null)
        {
            return UserErrors.NotFound(request.UserId);
        }

        return _mapper.Map<UserDetailsDto>(user);
    }
}