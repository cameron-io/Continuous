using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using API.Services.IRepositories;

namespace API.Services.Repositories;

public class PlayerRepository : IPlayerRepository
{
    protected DataContext _context;
    protected DbSet<Player> dbSet;
    protected readonly ILogger _logger;

    public PlayerRepository(
        DataContext context,
        ILogger logger
    )
    {
        _context = context;
        _logger = logger;
        this.dbSet = _context.Set<Player>();
    }

    public virtual async Task<IEnumerable<Player>> All() // virtual means that this method can be overriden by a class that inherits from this class
    {
        return await dbSet.ToListAsync();
    }

    public  virtual async Task<Player?> GetById(int id)
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

    public virtual async  Task<bool> Add(Player entity)
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

    public Task<bool> Upsert(Player entity)
    {
        throw new NotImplementedException();
    }
}