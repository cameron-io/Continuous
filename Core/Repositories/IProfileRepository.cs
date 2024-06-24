using Core.Data;

namespace Core.Repositories;

public interface IProfileRepository : IGenericRepository<Profile>
{
    Task<IReadOnlyList<Profile>> GetAllAsync();
    Task<Profile> GetByUserIdAsync(int id);
}