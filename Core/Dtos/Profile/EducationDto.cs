using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.Profile;

public class EducationDto
{
    [Required]
    public required string School { get; set; }
    [Required]
    public required string Degree { get; set; }
    [Required]
    public required string FieldOfStudy { get; set; }
    [Required]
    public required string From { get; set; }
    public DateTime? To { get; set; }
    public bool Current { get; set; } = false;
    public string Description { get; set; }
}
