namespace Domain.Entities;

public class Post: BaseEntity
{
    public string Text { get; set; }
    public string Name { get; set; }
    public List<UserLike> LikesByUsers { get; set; } = [];
    public List<Comment> Comments { get; } = [];
    
    public required int UserId { get; set; } // Required foreign key property
    public AppUser User { get; set; } = null!; // Required reference navigation to principal
}
