namespace LibraryManagementSystem.Common.DTOs.Requests;

public class IssueBookRequestDto
{
    public string MemberUniqueId { get; set; } = string.Empty;

    public string BookUniqueId { get; set; } = string.Empty;

    public int BorrowDays { get; set; } = 14;
}