using Core.Data;
using API.Dtos.Account;
using API.Dtos.Profile;

namespace API.Helpers;

public class AutoMapperProfiles : AutoMapper.Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, UserDto>();
        CreateMap<Profile, ProfileDto>()
            .ForMember(x => x.User, o => o.MapFrom(s => s.AppUser));
        CreateMap<ProfileDto, Profile>();
        CreateMap<Experience, ExperienceDto>();
        CreateMap<ExperienceDto, Experience>()
            .ForMember(x => x.To, o => o.MapFrom(s => s.To == "" ? null : s.To));
        CreateMap<Education, EducationDto>();
        CreateMap<EducationDto, Education>()
            .ForMember(x => x.To, o => o.MapFrom(s => s.To == "" ? null : s.To));
        CreateMap<Social, SocialDto>();
    }
}