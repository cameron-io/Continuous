namespace Core.Entities;

public class Post: BaseEntity
{
    public required int AccountId { get; set; } // Required foreign key property
    public Account Account { get; set; } = null!; // Required reference navigation to principal
    public string? Text { get; set; }
    public string? Name { get; set; }
    public ICollection<Like> Likes { get; } = [];
    public ICollection<Comment> Comments { get; } = [];
}
