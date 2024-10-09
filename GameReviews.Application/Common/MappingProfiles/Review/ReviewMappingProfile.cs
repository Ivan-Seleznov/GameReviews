using AutoMapper;
using GameReviews.Application.Common.Models.Dtos.Review;
using GameReviews.Application.Reviews.Commands.CreateReview;
using GameReviews.Domain.Entities.Review;

namespace GameReviews.Application.Common.MappingProfiles.Review;
internal class ReviewMappingProfile : Profile
{
    public ReviewMappingProfile()
    {
        CreateMap<ReviewEntity, ReviewDetailsDto>();
    }
}
