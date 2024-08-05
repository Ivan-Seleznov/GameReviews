using AutoMapper;
using GameReviews.Application.Common.Models.Dtos.User;
using GameReviews.Application.Users.Commands.CreateUser;
using GameReviews.Domain.Entities.User;

namespace GameReviews.Application.Common.MappingProfiles.User;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserEntity, UserDetailsDto>()
            .ForMember(dest =>
                dest.Id, opt =>
                opt.MapFrom(src => src.Id.Value));

        CreateMap<CreateUserCommand, UserEntity>();
    }
}