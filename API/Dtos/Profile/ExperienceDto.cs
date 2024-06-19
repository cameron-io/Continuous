namespace API.Dtos.Profile;

public class ExperienceDto
{
    public required string Title { get; set; }
    public required string Company { get; set; }
    public required string From { get; set; }
    public string Location { get; set; }
    public DateTime? To { get; set; }
    public bool Current { get; set; } = false;
    public string Description { get; set; }
}
