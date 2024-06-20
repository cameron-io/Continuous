using AutoMapper;
using API.Dtos.Profile;
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
    [HttpGet("me")]
    public async Task<ActionResult<ProfileDto>> GetMe()
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
        var profile = await unitOfWork.ProfileRepository.GetByUserIdAsync(user.Id);

        if (profile == null) return NotFound();

        return new ProfileDto
        {
            Status = profile.Status,
            Skills = profile.Skills,
            Company = profile.Company,
            Website = profile.Website,
            Location = profile.Location,
            Bio = profile.Bio,
            GitHubUsername = profile.GitHubUsername,
        };
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