using Core.Data;

namespace Core.Repositories;

public interface IProfileRepository : IGenericRepository<Profile>
{
    Task<Profile> GetByUserIdAsync(int id);
}