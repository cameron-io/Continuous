using Domain.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProfileRepository(DataContext context)
    : GenericRepository<Entities.Data.Profile>(context), IProfileRepository
{
    private readonly DataContext _context = context;

    public async Task<IReadOnlyList<Entities.Data.Profile>> GetAllAsync()
    {
        var query = _context.Profiles
            .Include(x => x.AppUser)
            .Include(x => x.Education)
            .Include(x => x.Experience)
            .Include(x => x.Social)
            .AsQueryable();
        
        return await query.ToListAsync();
    }

    public override async Task<Entities.Data.Profile> GetByIdAsync(int id)
    {
        var query = _context.Profiles
            .Where(x => x.Id == id)
            .Include(x => x.AppUser)
            .Include(x => x.Education)
            .Include(x => x.Experience)
            .Include(x => x.Social);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<Entities.Data.Profile> GetByUserIdAsync(int id)
    {
        return await _context.Profiles
            .Where(x => x.AppUserId == id)
            .Include(x => x.AppUser)
            .Include(x => x.Education)
            .Include(x => x.Experience)
            .Include(x => x.Social)
            .FirstOrDefaultAsync();
    }
}