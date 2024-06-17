namespace API.Dtos.Account;

public class UserDto
{
    public required string Email { get; set; }
    public required string DisplayName { get; set; }
    public required string Token { get; set; }
}