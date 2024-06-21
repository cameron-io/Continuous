using Core.Data;
using Core.Dtos.Profile;

namespace Core.Repositories;

public interface IProfileRepository : IGenericRepository<Profile>
{
    Task<IReadOnlyList<ProfileDto>> GetAllAsync();
    Task<Profile> GetByUserIdAsync(int id);
}