using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Domain.Entities;
using Domain.Services;
using API.Dtos.Profile;
using API.Extensions;

namespace API.Controllers;

[ApiController]
[Route("api/profiles")]
public class ProfileController(
    UserManager<AppUser> userManager,
    IMapper mapper,
    IProfileService profileService
) : BaseApiController
{
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<AppUser> _userManager = userManager;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProfileDto>>> GetAllProfiles()
    {
        var profiles = await profileService.ListAllProfilesAsync();

        if (profiles == null) return NotFound();

        return Ok(
            _mapper.Map<IReadOnlyList<Domain.Entities.Profile>, IReadOnlyList<ProfileDto>>(profiles)
        );
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProfileDto>> GetProfileById(int id)
    {
        var profile = await profileService.GetProfileIdAsync(id);

        if (profile == null) return NotFound();

        return _mapper.Map<Domain.Entities.Profile, ProfileDto>(profile);
    }

    [HttpGet("user/{id}")]
    public async Task<ActionResult<ProfileDto>> GetByUserId(int id)
    {
        var profile = await profileService.GetProfileByUserIdAsync(id);

        if (profile == null) return NotFound();

        return _mapper.Map<Domain.Entities.Profile, ProfileDto>(profile);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<ProfileDto>> GetMe()
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
        var profile = await profileService.GetProfileByUserIdAsync(user.Id);

        if (profile == null) return NotFound();

        return _mapper.Map<Domain.Entities.Profile, ProfileDto>(profile);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ProfileDto>> UpsertProfile(ProfileDto profileDto)
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

        var profile = _mapper.Map<ProfileDto, Domain.Entities.Profile>(profileDto);

        profile.AppUser = user;

        if (await profileService.UpsertAsync(profile)) return _mapper.Map<Domain.Entities.Profile, ProfileDto>(profile);

        return BadRequest("Failed to update user profile");
    }

    [Authorize]
    [HttpPut("experience")]
    public async Task<ActionResult<ProfileDto>> AddExperience(ExperienceDto experienceDto)
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
        var profile = await profileService.GetProfileByUserIdAsync(user.Id);
        var experience = _mapper.Map<ExperienceDto, Experience>(experienceDto);

        experience.Profile = profile;

        if (await profileService.UpsertExperienceAsync(experience))
        {
            var newProfile = await profileService.GetProfileByUserIdAsync(user.Id);
            return _mapper.Map<Domain.Entities.Profile, ProfileDto>(newProfile);
        }
        return BadRequest("Failed to update user profile");
    }

    [Authorize]
    [HttpPut("education")]
    public async Task<ActionResult<ProfileDto>> AddEducation(EducationDto educationDto)
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
        var profile = await profileService.GetProfileByUserIdAsync(user.Id);
        var education = _mapper.Map<EducationDto, Education>(educationDto);

        education.Profile = profile;

        if (await profileService.UpsertEducationAsync(education))
        {
            var newProfile = await profileService.GetProfileByUserIdAsync(user.Id);
            return _mapper.Map<Domain.Entities.Profile, ProfileDto>(newProfile);
        }
        return BadRequest("Failed to update user profile");
    }

    [Authorize]
    [HttpDelete("experience/{id}")]
    public async Task<ActionResult<ProfileDto>> DeleteExperience(int id)
    {
        if (await profileService.DeleteExperienceAsync(id))
        {
            var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
            var newProfile = await profileService.GetProfileByUserIdAsync(user.Id);
            return _mapper.Map<Domain.Entities.Profile, ProfileDto>(newProfile);
        }
        return BadRequest("Failed to update user profile");
    }

    [Authorize]
    [HttpDelete("education/{id}")]
    public async Task<ActionResult<ProfileDto>> DeleteEducation(int id)
    {
        if (await profileService.DeleteEducationAsync(id))
        {
            var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
            var newProfile = await profileService.GetProfileByUserIdAsync(user.Id);
            return _mapper.Map<Domain.Entities.Profile, ProfileDto>(newProfile);
        }
        return BadRequest("Failed to update user profile");
    }
}