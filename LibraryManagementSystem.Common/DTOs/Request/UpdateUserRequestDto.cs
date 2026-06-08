using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Common.DTOs.Requests;

public class UpdateUserRequestDto
{
    [Required(ErrorMessage = "Unique identifier is required.")]
    [StringLength(32)]
    public string RoleUniqueId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 200 characters.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = string.Empty;
}