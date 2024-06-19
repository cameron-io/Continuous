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
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<AppUser> _userManager = userManager;

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<ProfileDto>> GetMe()
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
        var profile = await _unitOfWork.Repository<Core.Data.Profile>().GetByIdAsync(user.Id);

        if (profile == null) return NotFound();

        return _mapper.Map<ProfileDto>(profile);
    }

    [Authorize]
    [HttpPost]
    public ActionResult UpsertProfile(ProfileDto profileDto)
    {
        var profile = new Core.Data.Profile
        {
            Status = profileDto.Status,
            Skills = profileDto.Skills,
            Company = profileDto.Company,
            Website = profileDto.Website,
            Location = profileDto.Location,
            Bio = profileDto.Bio,
            GitHubUsername = profileDto.GitHubUsername
        };
        
        _unitOfWork.Repository<Core.Data.Profile>().Upsert(profile);

        return Ok();
    }
}