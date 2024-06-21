using Core.Data;
using Core.Dtos.Profile;

namespace API.Helpers;

public class AutoMapperProfiles : AutoMapper.Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Profile, ProfileDto>()
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
            .ForMember(d => d.Skills, o => o.MapFrom(s => s.Skills))
            .ForMember(d => d.Company, o => o.MapFrom(s => s.Company))
            .ForMember(d => d.Website, o => o.MapFrom(s => s.Website))
            .ForMember(d => d.Location, o => o.MapFrom(s => s.Location))
            .ForMember(d => d.Bio, o => o.MapFrom(s => s.Bio))
            .ForMember(d => d.GitHubUsername, o => o.MapFrom(s => s.GitHubUsername))
            .ForMember(d => d.Education, o => o.MapFrom(s => s.Education))
            .ForMember(d => d.Experience, o => o.MapFrom(s => s.Experience))
            .ForMember(d => d.Social, o => o.MapFrom(s => s.Social));
        CreateMap<Experience, ExperienceDto>();
        CreateMap<Education, EducationDto>();
        CreateMap<Social, SocialDto>();
    }       
}