public class FineDetailsView
{
    public string FineUniqueId { get; set; } = string.Empty;

    public string MemberUniqueId { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string BookUniqueId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public decimal FineAmount { get; set; }

    public bool IsPaid { get; set; }

    public DateTime? PaidOn { get; set; }

    public DateTime CreatedOn { get; set; }
}