using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.Profile;

public class ExperienceDto
{
    [Required]
    public required string Title { get; set; }
    [Required]
    public required string Company { get; set; }
    [Required]
    public required string From { get; set; }
    public string Location { get; set; }
    public DateTime? To { get; set; }
    public bool Current { get; set; } = false;
    public string Description { get; set; }
}
