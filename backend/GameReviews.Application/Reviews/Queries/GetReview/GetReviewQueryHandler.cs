using AutoMapper;
using GameReviews.Application.Common.Errors;
using GameReviews.Application.Common.Interfaces;
using GameReviews.Application.Common.Interfaces.Query;
using GameReviews.Application.Common.Models.Dtos.Review;
using GameReviews.Application.Reviews.Queries.GetReviews;
using GameReviews.Domain.Results;
using Microsoft.EntityFrameworkCore;

namespace GameReviews.Application.Reviews.Queries.GetReview;

internal class GetReviewQueryHandler : IQueryHandler<GetReviewQuery,ReviewDetailsDto>
{
    private readonly IReadApplicationDbContext _context;
    private readonly IMapper _mapper;
    
    public GetReviewQueryHandler(IReadApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Result<ReviewDetailsDto>> Handle(GetReviewQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Reviews
            .Include(r => r.Game)
            .Include(r => r.Author)
            .FirstOrDefaultAsync(r => r.Id == request.ReviewId, cancellationToken);

        if (result is null)
        {
            return ReviewErrors.NotFound(request.ReviewId);
        }
        
        return _mapper.Map<ReviewDetailsDto>(result);
    }
}