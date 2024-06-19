using Core.Data;

namespace API.Dtos.Profile;

public class ProfileDto
{
    public required string Status { get; set; }
    public List<string> Skills { get; set; } = [];

    public string Company { get; set; }
    public string Website { get; set; }
    public string Location { get; set; }
    public string Bio { get; set; }
    public string GitHubUsername { get; set; }
    public Experience Experience { get; set; }
    public Education Education { get; set; }
    public Social Social { get; set; }
}
