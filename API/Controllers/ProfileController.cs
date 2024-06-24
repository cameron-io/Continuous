using AutoMapper;
using Core.Dtos.Profile;
using API.Extensions;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController(
    UserManager<AppUser> userManager,
    IMapper mapper,
    IUnitOfWork unitOfWork
) : BaseApiController
{
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<AppUser> _userManager = userManager;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProfileDto>>> GetAllProfiles()
    {
        var profiles = await unitOfWork.ProfileRepository.GetAllAsync();

        if (profiles == null) return NotFound();

        return Ok(
            _mapper.Map<IReadOnlyList<Core.Data.Profile>, IReadOnlyList<ProfileDto>>(profiles)
        );
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProfileDto>> GetProfileById(int id)
    {
        var profile = await unitOfWork.ProfileRepository.GetByIdAsync(id);

        if (profile == null) return NotFound();

        return _mapper.Map<Core.Data.Profile, ProfileDto>(profile);
    }
    
    [HttpGet("user/{id}")]
    public async Task<ActionResult<ProfileDto>> GetByUserId(int id)
    {
        var profile = await unitOfWork.ProfileRepository.GetByUserIdAsync(id);

        if (profile == null) return NotFound();

        return _mapper.Map<Core.Data.Profile, ProfileDto>(profile);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<ProfileDto>> GetMe()
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
        var profile = await unitOfWork.ProfileRepository.GetByUserIdAsync(user.Id);

        if (profile == null) return NotFound();

        return _mapper.Map<Core.Data.Profile, ProfileDto>(profile);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ProfileDto>> UpsertProfile(ProfileDto profileDto)
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
        var profile = new Core.Data.Profile
        {
            Status = profileDto.Status,
            Skills = profileDto.Skills,
            Company = profileDto.Company,
            Website = profileDto.Website,
            Location = profileDto.Location,
            Bio = profileDto.Bio,
            GitHubUsername = profileDto.GitHubUsername,
            AppUser = user
        };
        
        unitOfWork.ProfileRepository.Upsert(profile);
        if (await unitOfWork.Complete()) return Ok();

        return BadRequest("Failed to update user profile");
    }

    [Authorize]
    [HttpPut("experience")]
    public async Task<ActionResult<ExperienceDto>> AddExperience(ExperienceDto experienceDto)
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
        var profile = await unitOfWork.ProfileRepository.GetByUserIdAsync(user.Id);
        
        var experience = new Experience
        {
            
            Title = experienceDto.Title,
            Company = experienceDto.Company,
            Location = experienceDto.Location,
            From = experienceDto.From,
            To = experienceDto.To == "" ? null : experienceDto.To,
            Current = experienceDto.Current,
            Description = experienceDto.Description,
            Profile = profile
        };
        
        unitOfWork.Repository<Experience>().Upsert(experience);
        if (await unitOfWork.Complete()) return Ok();

        return BadRequest("Failed to update user profile");
    }
    
    [Authorize]
    [HttpPut("education")]
    public async Task<ActionResult<EducationDto>> AddEducation(EducationDto educationDto)
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
        var profile = await unitOfWork.ProfileRepository.GetByUserIdAsync(user.Id);
        
        var education = new Education
        {
            School = educationDto.School,
            Degree = educationDto.Degree,
            FieldOfStudy = educationDto.FieldOfStudy,
            From = educationDto.From,
            To = educationDto.To == "" ? null : educationDto.To,
            Current = educationDto.Current,
            Description = educationDto.Description,
            Profile = profile
        };

        unitOfWork.Repository<Education>().Upsert(education);
        if (await unitOfWork.Complete()) return Ok();

        return BadRequest("Failed to update user profile");
    }

    [Authorize]
    [HttpDelete("experience/{id}")]
    public async Task<ActionResult<ExperienceDto>> DeleteExperience(int id)
    {
        var experience = await unitOfWork.Repository<Experience>().GetByIdAsync(id);
        unitOfWork.Repository<Experience>().Delete(experience);
        if (await unitOfWork.Complete()) return Ok();

        return BadRequest("Failed to update user profile");
    }

    [Authorize]
    [HttpDelete("education/{id}")]
    public async Task<ActionResult<EducationDto>> DeleteEducation(int id)
    {
        var education = await unitOfWork.Repository<Education>().GetByIdAsync(id);
        unitOfWork.Repository<Education>().Delete(education);
        if (await unitOfWork.Complete()) return Ok();

        return BadRequest("Failed to update user profile");
    }
}