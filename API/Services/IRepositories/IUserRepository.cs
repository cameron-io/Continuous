using API.Models;

namespace API.Services.IRepositories;

public interface IUserRepository : IGenericRepository<User>
{
    // add methods that are specific to the User entity
    // e.g Task<User> GetByEmail(string email);
    // e.g Task<User> GetByName(string name);
    // e.g Task<User> GetByEmailAndPassword(string email, string password);
}
