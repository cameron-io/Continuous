namespace Core.Entities;

public class Education: BaseEntity
{
    public required string School { get; set; }
    public required string Degree { get; set; }
    public required string FieldOfStudy { get; set; }
    public required string From { get; set; }
    public DateTime? To { get; set; }
    public bool? Current { get; set; }
    public string? Description { get; set; }
}
