namespace Core.Entities;

public class Comment: BaseEntity
{
    public required Account user { get; set; }
    public required string text { get; set; }
    public DateTime? date { get; set; }
}
