using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using API.Services.IRepositories;

namespace API.Services.Repositories;

public class AccountRepository : IAccountRepository
{
    protected DataContext _context;
    protected DbSet<Account> dbSet;
    protected readonly ILogger _logger;

    public AccountRepository(
        DataContext context,
        ILogger logger
    )
    {
        _context = context;
        _logger = logger;
        this.dbSet = _context.Set<Account>();
    }

    public virtual async Task<IEnumerable<Account>> All() // virtual means that this method can be overriden by a class that inherits from this class
    {
        return await dbSet.ToListAsync();
    }

    public  virtual async Task<Account?> GetById(int id)
    {
        try
        {
            return await dbSet.FindAsync(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting entity with id {Id}", id);
            return null;
        }
    }

    public virtual async  Task<bool> Add(Account entity)
    {
        try
        {
            await dbSet.AddAsync(entity);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error adding entity");
            return false;
        }
    }

   public virtual async Task<bool> Delete(int id)
    {
        try
        {
            var entity = await dbSet.FindAsync(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
                return true;
            }
            else
            {
                _logger.LogWarning("Entity with id {Id} not found for deletion", id);
                return false;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting entity with id {Id}", id);
            return false;
        }
    }

    public Task<bool> Upsert(int id, Account entity)
    {
        throw new NotImplementedException();
    }
}