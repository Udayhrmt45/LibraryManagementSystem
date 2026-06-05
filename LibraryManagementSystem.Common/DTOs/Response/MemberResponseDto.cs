namespace LibraryManagementSystem.Common.DTOs.Responses;

public class MemberResponseDto
{
    public string UniqueId { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public DateTime MembershipDate { get; set; }
}