using AutoMapper;
using GameReviews.Application.Common.Models.Dtos.Jwt;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Application.Common.MappingProfiles.Jwt;

public class JwtMappingProfile : Profile
{
    public JwtMappingProfile()
    {
        CreateMap<UserEntity, JwtTokenGenerateRequestDto>();
    }
}