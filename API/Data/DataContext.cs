using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data;

public class DataContext : DbContext {
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {

    }

    // DbSets are used to query and save instances of entities to a database
    // Here the DbSet is of type Account and the name of the table in the database will be Players
    public DbSet<Account> Accounts { get; set; }
}
