using System.ComponentModel.DataAnnotations;
namespace LibraryManagementSystem.Common.DTOs.Requests;

public class LoginRequestDto
{
    [Required]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
    ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}