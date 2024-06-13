using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Infrastructure.Data;

public class DataContext : DbContext {
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {

    }

    // DbSets are used to query and save instances of entities to a database
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Post> Posts { get; set; }
}
