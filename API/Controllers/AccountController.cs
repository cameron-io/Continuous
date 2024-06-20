using API.Dtos.Account;
using API.Errors;
using API.Extensions;
using Core.Data;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api")]
public class AccountController(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    ITokenService tokenService) : BaseApiController
{

    [Authorize]
    [HttpGet("auth")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await userManager.FindByEmailFromClaimsPrincipal(User);

        return new UserDto
        {
            Email = user.Email,
            Token = tokenService.CreateToken(user),
            DisplayName = user.Name
        };
    }

    [HttpPost("auth")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);

        if (user == null) return Unauthorized(new ApiResponse(401));

        var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

        Response.Cookies.Append("token", tokenService.CreateToken(user), new CookieOptions {
            Expires = DateTime.Now.AddHours(3),
            HttpOnly = true
        });

        return Ok();
    }

    [HttpPost("users")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
        {
            return new BadRequestObjectResult(new ApiValidationErrorResponse 
                { Errors = ["Email address is in use"] });
        }

        var user = new AppUser
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            UserName = registerDto.Email
        };

        var result = await userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) return BadRequest(new ApiResponse(400));

        return await Login(new LoginDto
        {
            Email = registerDto.Email,
            Password = registerDto.Password
        });
    }

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    {
        return await userManager.FindByEmailAsync(email) != null;
    }

}