namespace Core.Entities;

public class Social: BaseEntity
{
    public required int ProfileId { get; set; } // Required foreign key property
    public Profile Profile { get; set; } = null!; // Required reference navigation to principal
    
    public string? YouTube { get; set; }
    public string? Twitter { get; set; }
    public string? Facebook { get; set; }
    public string? LinkedIn { get; set; }
    public string? Instagram { get; set; }
}
