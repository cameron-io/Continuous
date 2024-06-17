using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Account;

public class RegisterDto
{
    [Required]
    public required string DisplayName { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [RegularExpression(
        "(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
        ErrorMessage =
            "Password must have " +
            "1 Uppercase, 1 Lowercase, 1 Number, 1 Special Character " + 
            "and at least 6 characters")]
    public required string Password { get; set; }
}