using Core.Data;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Context;

namespace Infrastructure.Repositories;

public class ProfileRepository(DataContext context)
    : GenericRepository<Profile>(context), IProfileRepository
{
    private readonly DataContext _context = context;

    public async Task<Profile> GetByUserIdAsync(int id)
    {
        return await _context.Profiles
            .Where(x => x.AppUserId == id)
            .Include(x => x.Education)
            .Include(x => x.Experience)
            .Include(x => x.Social)
            .FirstOrDefaultAsync();
    }
}