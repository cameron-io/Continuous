using Core.Data;
using Core.Dtos.Account;
using Core.Dtos.Profile;

namespace API.Helpers;

public class AutoMapperProfiles : AutoMapper.Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, UserDto>();
        CreateMap<Profile, ProfileDto>()
            .ForMember(x => x.User, o => o.MapFrom(s => s.AppUser));
        CreateMap<Experience, ExperienceDto>();
        CreateMap<Education, EducationDto>();
        CreateMap<Social, SocialDto>();
    }       
}