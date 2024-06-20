using System.ComponentModel.DataAnnotations;
using Core.Data;

namespace API.Dtos.Profile;

public class ProfileDto
{
    [Required]
    public required string Status { get; set; }
    [Required]
    public List<string> Skills { get; set; } = [];

    public string Company { get; set; }
    public string Website { get; set; }
    public string Location { get; set; }
    public string Bio { get; set; }
    public string GitHubUsername { get; set; }
    public List<ExperienceDto> Experience { get; set; } = [];
    public List<Education> Education { get; set; } = [];
    public List<Social> Social { get; set; } = [];
}
