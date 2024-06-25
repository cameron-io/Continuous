using Domain.Entities;

namespace Domain.Services;

public interface ITokenService
{
    string CreateToken(AppUser user);
}