using AutoMapper;
using GameReviews.Application.Common.Models.Dtos.Jwt;
using GameReviews.Domain.Entities.UserAggregate;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Application.Common.MappingProfiles.Jwt;

public class JwtMappingProfile : Profile
{
    public JwtMappingProfile()
    {
        CreateMap<UserEntity, JwtTokenGenerateRequestDto>();
    }
}