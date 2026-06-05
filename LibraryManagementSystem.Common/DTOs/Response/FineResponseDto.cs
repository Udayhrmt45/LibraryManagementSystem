namespace LibraryManagementSystem.Common.DTOs.Responses;

public class FineResponseDto
{
    public string UniqueId { get; set; } = string.Empty;

    public decimal FineAmount { get; set; }

    public bool IsPaid { get; set; }

    public DateTime? PaidOn { get; set; }

    public string IssueUniqueId { get; set; } = string.Empty;
}