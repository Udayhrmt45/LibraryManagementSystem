namespace LibraryManagementSystem.Common.DTOs.Responses;

public class BookIssueResponseDto
{
    public string IssueUniqueId { get; set; } = string.Empty;

    public string MemberName { get; set; } = string.Empty;

    public string BookTitle { get; set; } = string.Empty;

    public DateTime IssueDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public string Status { get; set; } = string.Empty;
}