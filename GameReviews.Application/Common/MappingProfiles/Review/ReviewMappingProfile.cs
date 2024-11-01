using AutoMapper;
using GameReviews.Application.Common.Extensions;
using GameReviews.Application.Common.Models.Dtos.Review;
using GameReviews.Application.Common.Models.ReadEntities;

namespace GameReviews.Application.Common.MappingProfiles.Review;
internal class ReviewMappingProfile : Profile
{
    public ReviewMappingProfile()
    {
        CreateMap<ReviewReadEntity, ReviewDetailsDto>()
            .IncludeMembers(r => r.Game)
            .IncludeMembers(r => r.Author)
            .MemberMapFrom(src => src.Id, dest => dest.Id.Value);

        CreateMap<UserReadEntity, ReviewDetailsDto>();
        CreateMap<GameReadEntity, ReviewDetailsDto>();
    }
}
