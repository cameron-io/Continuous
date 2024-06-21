using Microsoft.AspNetCore.Identity;

namespace Core.Data;

public class AppRole : IdentityRole<int>
{
    public ICollection<AppUserRole> UserRoles { get; set; } = [];
}