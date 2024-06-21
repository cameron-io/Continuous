using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Context;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos.Profile;

namespace Infrastructure.Repositories;

public class ProfileRepository(DataContext context, IMapper mapper)
    : GenericRepository<Core.Data.Profile>(context), IProfileRepository
{
    private readonly DataContext _context = context;
    
    public async Task<IReadOnlyList<ProfileDto>> GetAllAsync()
    {
        var query = _context.Profiles
            .Include(x => x.Education)
            .Include(x => x.Experience)
            .Include(x => x.Social)
            .AsQueryable()
            .ProjectTo<ProfileDto>(mapper.ConfigurationProvider);
        
        return await query.ToListAsync();
    }

    public async Task<Core.Data.Profile> GetByUserIdAsync(int id)
    {
        return await _context.Profiles
            .Where(x => x.AppUserId == id)
            .Include(x => x.Education)
            .Include(x => x.Experience)
            .Include(x => x.Social)
            .FirstOrDefaultAsync();
    }
}