namespace LibraryManagementSystem.Common.DTOs.Requests;

public class UpdateUserRequestDto
{
    public string RoleUniqueId { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}