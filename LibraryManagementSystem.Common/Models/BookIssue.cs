namespace LibraryManagementSystem.Common.Models;

public class BookIssue : BaseModel
{
    public int IssueId { get; set; }

    public int MemberId { get; set; }

    public int BookId { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public string Status { get; set; } = string.Empty;

    // Display Only

    public string MemberName { get; set; } = string.Empty;

    public string BookTitle { get; set; } = string.Empty;
}