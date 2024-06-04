using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using API.Services.IRepositories;

namespace API.Services.Repositories;

public class UserRepository : IUserRepository
{
    protected DataContext _context;
    protected DbSet<User> dbSet;
    protected readonly ILogger _logger;

    public UserRepository(
        DataContext context,
        ILogger logger
    )
    {
        _context = context;
        _logger = logger;
        this.dbSet = _context.Set<User>();
    }

    public virtual async Task<IEnumerable<User>> All() // virtual means that this method can be overriden by a class that inherits from this class
    {
        return await dbSet.ToListAsync();
    }

    public  virtual async Task<User?> GetById(int id)
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

    public virtual async  Task<bool> Add(User entity)
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

    public Task<bool> Upsert(int id, User entity)
    {
        throw new NotImplementedException();
    }
}