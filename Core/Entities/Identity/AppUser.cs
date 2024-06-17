using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class AppUser : IdentityUser
{
    public required string DisplayName { get; set; }
    public Profile Profile { get; set; }
    public List<Post> Post { get; set; }
    public List<Comment> Comments { get; set; }
    public List<UserLike> UserLikes { get; set; }
}