namespace Core.Dtos.Account;

public class UserDto
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string DisplayName { get; set; }
    public required string Token { get; set; }
    public string Avatar { get; set; }
}