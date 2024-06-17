using Core.Entities;

namespace API.Dtos.Account;

public class ProfileDto
{
    public required string Status { get; set; }
    public List<string> Skills { get; set; } = [];

    public string Company { get; set; }
    public string Website { get; set; }
    public string Location { get; set; }
    public string Bio { get; set; }
    public string GitHubUsername { get; set; }
    public required Experience Experience { get; set; }
    public required Education Education { get; set; }
    public required Social Social { get; set; }
}
