using AutoMapper;
using GameReviews.Application.Common.Extensions;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Domain.Entities.Game;

namespace GameReviews.Application.Common.MappingProfiles.Game;
internal class GameMappingProfile : Profile
{
    public GameMappingProfile()
    {
        CreateMap<GameDetailsDto, GameEntity>();
        CreateMap<GameEntity, GameDetailsDto>();

        /*
         * .ForMember(dest =>
           dest.Id, opt =>
           opt.MapFrom(src => src.Id.Value));
         */

        CreateMap<GameEntity, GameInfoDto>().MemberMapFrom(src => src.Id, dest => dest.Id.Value); ;
    }
}