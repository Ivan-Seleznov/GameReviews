using AutoMapper;
using GameReviews.Application.Common.Extensions;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Common.Models.Dtos.Review;
using GameReviews.Application.Common.Models.Dtos.User;
using GameReviews.Application.Reviews.Commands.CreateReview;
using GameReviews.Domain.Entities.Game;
using GameReviews.Domain.Entities.Review;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Application.Common.MappingProfiles.Review;
internal class ReviewMappingProfile : Profile
{
    public ReviewMappingProfile()
    {
        CreateMap<ReviewEntity, ReviewDetailsDto>()
            .IncludeMembers(r => r.Game)
            .IncludeMembers(r => r.Author)
            .MemberMapFrom(src => src.Id, dest => dest.Id.Value);

        CreateMap<UserEntity, ReviewDetailsDto>();
        CreateMap<GameEntity, ReviewDetailsDto>();
    }
}
