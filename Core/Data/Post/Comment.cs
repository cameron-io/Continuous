namespace Core.Data;

public class Comment : BaseEntity
{
    public required string CommenterUsername { get; set; }
    public required string Content { get; set; }
    public DateTime CommentedAt { get; set; } = DateTime.UtcNow;

    // navigation properties
    public int CommenterId { get; set; }
    public AppUser Commenter { get; set; } = null!;
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
}
