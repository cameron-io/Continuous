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
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProfileDto>>> GetAllProfiles()
    {
        var profiles = await unitOfWork.ProfileRepository.GetAllAsync();

        if (profiles == null) return NotFound();

        return Ok(profiles);
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
    public async Task<ActionResult<ProfileDto>> CreateProfile(ProfileDto profileDto)
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
            AppUserId = user.Id,
            AppUser = user
        };
        
        unitOfWork.ProfileRepository.Add(profile);
        if (await unitOfWork.Complete()) return Ok();

        return BadRequest("Failed to update user profile");
    }
}