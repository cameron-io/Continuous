using System.Reflection;

namespace Infrastructure.Data.Context;

public class DataContextSeed
{
    public static async Task SeedAsync(DataContext context)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
    }
}