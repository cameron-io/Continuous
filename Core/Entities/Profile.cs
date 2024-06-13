namespace Core.Entities;

public class Profile: BaseEntity
{
    public required string Status { get; set; }
    public required string[] Skills { get; set; }
    
    public string? Company { get; set; }
    public string? Website { get; set; }
    public string? Location { get; set; }
    public string? Bio { get; set; }
    public string? GitHubUsername { get; set; }
    public required Experience Experience { get; set; }
    public required Education Education { get; set; }
    public required Social Social { get; set; }
    public required DateTime Date { get; set; }
}
