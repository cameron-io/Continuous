using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Profile;

public class ExperienceDto
{
    public int Id { get; set; }
    [Required]
    public required string Title { get; set; }
    [Required]
    public required string Company { get; set; }
    [Required]
    public required string From { get; set; }
    public string Location { get; set; }
    public string To { get; set; } = null;
    public bool Current { get; set; } = false;
    public string Description { get; set; }
}
