namespace Core.Entities;

public class Post: BaseEntity
{
    public required string Text { get; set; }
    public required string Name { get; set; }
    public Account[]? likes { get; set; }
    public Comment[]? comments { get; set; }
    public DateTime? date { get; set; }
}
