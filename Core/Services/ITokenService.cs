using Core.Data;

namespace Core.Services;

public interface ITokenService
{
    string CreateToken(AppUser user);
}