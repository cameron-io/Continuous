namespace Core.Entities;

public class Account: BaseEntity
{
    public string? Name { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public Profile? Profile { get; set; }
}
