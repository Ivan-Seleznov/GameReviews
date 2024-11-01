using AutoMapper;
using GameReviews.Application.Common.Extensions;
using GameReviews.Application.Common.Models.Dtos.Game;
using GameReviews.Application.Common.Models.ReadEntities;
using GameReviews.Domain.Entities.GameAggregate.Entities;

namespace GameReviews.Application.Common.MappingProfiles.Game;
internal class GameMappingProfile : Profile
{
    public GameMappingProfile()
    { 
        //Read
        CreateMap<GameReadEntity, GameDetailsDto>();
        CreateMap<GameReadEntity, GameInfoDto>().MemberMapFrom(src => src.Id, dest => dest.Id.Value);
        
        //Write
        CreateMap<GameEntity, GameDetailsDto>();
        CreateMap<GameEntity, GameInfoDto>();
    }
}