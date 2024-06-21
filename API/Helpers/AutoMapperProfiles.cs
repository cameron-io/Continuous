using Core.Data;
using Core.Dtos.Profile;

namespace API.Helpers;

public class AutoMapperProfiles : AutoMapper.Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Profile, ProfileDto>();
        CreateMap<Experience, ExperienceDto>();
        CreateMap<Education, EducationDto>();
        CreateMap<Social, SocialDto>();
    }       
}