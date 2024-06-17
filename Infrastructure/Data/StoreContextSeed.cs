using System.Reflection;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
    }
}