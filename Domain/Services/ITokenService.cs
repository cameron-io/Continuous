using Entities.Data;

namespace Domain.Services;

public interface ITokenService
{
    string CreateToken(AppUser user);
}