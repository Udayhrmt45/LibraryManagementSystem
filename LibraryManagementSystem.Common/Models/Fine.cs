using LibraryManagementSystem.Common.Models;
public class Fine : BaseModel
{
    public int FineId { get; set; }

    public int IssueId { get; set; }

    public decimal FineAmount { get; set; }

    public bool IsPaid { get; set; }

    public DateTime? PaidOn { get; set; }

    public string IssueUniqueId { get; set; } = string.Empty;
}