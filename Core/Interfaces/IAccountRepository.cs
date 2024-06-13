using Core.Entities;

namespace Core.Interfaces;

public interface IAccountRepository : IGenericRepository<Account>
{
    // add methods that are specific to the Account entity
    // e.g Task<Account> GetByEmail(string email);
    // e.g Task<Account> GetByName(string name);
    // e.g Task<Account> GetByEmailAndPassword(string email, string password);
}
