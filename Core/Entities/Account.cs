namespace Core.Entities;

public class Account: BaseEntity
{
    public required string Name { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
}
