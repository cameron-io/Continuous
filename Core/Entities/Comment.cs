namespace Core.Entities;

public class Comment: BaseEntity
{
    public required int PostId { get; set; } // Required foreign key property
    public Post Post { get; set; } = null!;
    public required int AccountId { get; set; }
    public Account Account { get; set; } = null!;
    
    public string? Text { get; set; }
}
