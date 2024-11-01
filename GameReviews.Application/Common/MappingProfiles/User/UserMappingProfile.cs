using AutoMapper;
using GameReviews.Application.Common.Extensions;
using GameReviews.Application.Common.Models.Dtos.User;
using GameReviews.Application.Common.Models.ReadEntities;
using GameReviews.Application.Users.Commands.CreateUser;
using GameReviews.Domain.Common.Abstractions.Entities;
using GameReviews.Domain.Entities.UserAggregate;
using GameReviews.Domain.Entities.UserAggregate.Entities;

namespace GameReviews.Application.Common.MappingProfiles.User;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        //Write
        CreateMap<UserEntity, UserDetailsDto>();
        CreateMap<UserEntity, UserInfoDto>().MemberMapFrom(src => src.Id, dest => dest.Id.Value); ;

        //Read
        CreateMap<UserReadEntity, UserDetailsDto>();
        CreateMap<UserReadEntity, UserInfoDto>().MemberMapFrom(src => src.Id, dest => dest.Id.Value);

        //Other
        CreateMap<CreateUserCommand, UserEntity>();
    }
}