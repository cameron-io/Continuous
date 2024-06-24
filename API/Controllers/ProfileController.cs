using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Entities.Data;
using Domain.Services;
using Application.Dtos.Profile;
using API.Extensions;

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
            _mapper.Map<IReadOnlyList<Entities.Data.Profile>, IReadOnlyList<ProfileDto>>(profiles)
        );
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProfileDto>> GetProfileById(int id)
    {
        var profile = await unitOfWork.ProfileRepository.GetByIdAsync(id);

        if (profile == null) return NotFound();

        return _mapper.Map<Entities.Data.Profile, ProfileDto>(profile);
    }
    
    [HttpGet("user/{id}")]
    public async Task<ActionResult<ProfileDto>> GetByUserId(int id)
    {
        var profile = await unitOfWork.ProfileRepository.GetByUserIdAsync(id);

        if (profile == null) return NotFound();

        return _mapper.Map<Entities.Data.Profile, ProfileDto>(profile);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<ProfileDto>> GetMe()
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
        var profile = await unitOfWork.ProfileRepository.GetByUserIdAsync(user.Id);

        if (profile == null) return NotFound();

        return _mapper.Map<Entities.Data.Profile, ProfileDto>(profile);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ProfileDto>> UpsertProfile(ProfileDto profileDto)
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

        var profile = _mapper.Map<ProfileDto, Entities.Data.Profile>(profileDto);

        profile.AppUser = user;
        
        unitOfWork.ProfileRepository.Upsert(profile);
        if (await unitOfWork.Complete()) return Ok();

        return BadRequest("Failed to update user profile");
    }

    [Authorize]
    [HttpPut("experience")]
    public async Task<ActionResult<ProfileDto>> AddExperience(ExperienceDto experienceDto)
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
        var profile = await unitOfWork.ProfileRepository.GetByUserIdAsync(user.Id);
        var experience = _mapper.Map<ExperienceDto, Experience>(experienceDto);

        experience.Profile = profile;
        unitOfWork.Repository<Experience>().Upsert(experience);

        if (await unitOfWork.Complete())
        {
            var newProfile = await unitOfWork.ProfileRepository.GetByUserIdAsync(user.Id);
            return _mapper.Map<Entities.Data.Profile, ProfileDto>(newProfile);
        }
        return BadRequest("Failed to update user profile");
    }

    [Authorize]
    [HttpPut("education")]
    public async Task<ActionResult<ProfileDto>> AddEducation(EducationDto educationDto)
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
        var profile = await unitOfWork.ProfileRepository.GetByUserIdAsync(user.Id);
        var education = _mapper.Map<EducationDto, Education>(educationDto);

        education.Profile = profile;
        unitOfWork.Repository<Education>().Upsert(education);

        if (await unitOfWork.Complete())
        {
            var newProfile = await unitOfWork.ProfileRepository.GetByUserIdAsync(user.Id);
            return _mapper.Map<Entities.Data.Profile, ProfileDto>(newProfile);
        }
        return BadRequest("Failed to update user profile");
    }

    [Authorize]
    [HttpDelete("experience/{id}")]
    public async Task<ActionResult<ProfileDto>> DeleteExperience(int id)
    {
        var experience = await unitOfWork.Repository<Experience>().GetByIdAsync(id);
        unitOfWork.Repository<Experience>().Delete(experience);

        if (await unitOfWork.Complete())
        {
            var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
            var newProfile = await unitOfWork.ProfileRepository.GetByUserIdAsync(user.Id);
            return _mapper.Map<Entities.Data.Profile, ProfileDto>(newProfile);
        }
        return BadRequest("Failed to update user profile");
    }

    [Authorize]
    [HttpDelete("education/{id}")]
    public async Task<ActionResult<ProfileDto>> DeleteEducation(int id)
    {
        var education = await unitOfWork.Repository<Education>().GetByIdAsync(id);
        unitOfWork.Repository<Education>().Delete(education);

        if (await unitOfWork.Complete())
        {
            var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
            var newProfile = await unitOfWork.ProfileRepository.GetByUserIdAsync(user.Id);
            return _mapper.Map<Entities.Data.Profile, ProfileDto>(newProfile);
        }
        return BadRequest("Failed to update user profile");
    }
}