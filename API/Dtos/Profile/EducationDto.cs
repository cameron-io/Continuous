using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Profile;

public class EducationDto
{
    public int Id { get; set; }
    [Required]
    public required string School { get; set; }
    [Required]
    public required string Degree { get; set; }
    [Required]
    public required string FieldOfStudy { get; set; }
    [Required]
    public required string From { get; set; }
    public string To { get; set; } = null;
    public bool Current { get; set; } = false;
    public string Description { get; set; }
}
