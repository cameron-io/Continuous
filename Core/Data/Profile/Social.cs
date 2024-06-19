namespace Core.Data;

public class Social: BaseEntity
{
    public string YouTube { get; set; }
    public string Twitter { get; set; }
    public string Facebook { get; set; }
    public string LinkedIn { get; set; }
    public string Instagram { get; set; }
    
    public int ProfileId { get; set; } // Required foreign key property
    public Profile Profile { get; set; } = null!; // Required reference navigation to principal
}
