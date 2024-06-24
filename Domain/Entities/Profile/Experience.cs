namespace Entities.Data;

public class Experience: BaseEntity
{    
    public string Title { get; set; }
    public string Company { get; set; }
    public string Location { get; set; }
    public string From { get; set; }
    public string To { get; set; } = null;
    public bool Current { get; set; } = false;
    public string Description { get; set; }
    
    public int ProfileId { get; set; } // Required foreign key property
    public Profile Profile { get; set; } = null!; // Required reference navigation to principal
}
