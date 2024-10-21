using AutoMapper;
using GameReviews.Application.Common.Extensions;
using GameReviews.Application.Common.Models.Dtos.User;
using GameReviews.Application.Users.Commands.CreateUser;
using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Application.Common.MappingProfiles.User;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserEntity, UserDetailsDto>();
        CreateMap<UserEntity, UserInfoDto>().MemberMapFrom(src => src.Id, dest => dest.Id.Value); ;

        CreateMap<CreateUserCommand, UserEntity>();
    }
}