namespace Core.Entities;

public class Profile: BaseEntity
{
    public required int AccountId { get; set; } // Required foreign key property
    public required Account Account { get; set; } = null!; // Required reference navigation to principal

    public string? Status { get; set; }
    public ICollection<String> Skills { get; set; } = [];

    public string? Company { get; set; }
    public string? Website { get; set; }
    public string? Location { get; set; }
    public string? Bio { get; set; }
    public string? GitHubUsername { get; set; }
    public Experience Experience { get; set; } = null!;
    public Education Education { get; set; } = null!;
    public Social Social { get; set; } = null!;
}
