using Core.Data;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.Context;

public class IdentityDbContextSeed
{
    public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser
            {
                DisplayName = "Bob",
                Email = "bob@test.com",
                UserName = "bob@test.com"
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }
}