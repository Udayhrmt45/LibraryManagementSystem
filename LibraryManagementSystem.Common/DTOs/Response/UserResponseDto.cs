namespace LibraryManagementSystem.Common.DTOs.Responses;

public class UserResponseDto
{
    public string UniqueId { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string RoleName { get; set; } = string.Empty;
}