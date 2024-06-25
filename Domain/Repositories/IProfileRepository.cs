using Domain.Entities;

namespace Domain.Repositories;

public interface IProfileRepository : IGenericRepository<Profile>
{
    Task<IReadOnlyList<Profile>> GetAllAsync();
    Task<Profile> GetByUserIdAsync(int id);
}