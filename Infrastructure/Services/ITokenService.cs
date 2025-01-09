using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public interface ITokenService<T> where T : IdentityUser<int>
{
    string CreateToken(T user);
}