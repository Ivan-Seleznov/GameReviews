using AutoMapper;
using GameReviews.Application.Common.Errors;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.User;
using GameReviews.Domain.Results;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Application.Users.Queries.GetUser;
public class GetUserQueryHandler : IQueryHandler<GetUserQuery,UserDetailsDto>
{
    private readonly IReadApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IReadApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result<UserDetailsDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        if (user is null)
        {
            return UserErrors.NotFound(request.UserId);
        }

        return _mapper.Map<UserDetailsDto>(user);
    }
}