using LibraryManagementSystem.Common.Models;

namespace LibraryManagementSystem.Common.DTOs.Responses;

public class FineDetailsResponseDto 
{
    public string FineUniqueId { get; set; } = string.Empty;

    public string MemberUniqueId { get; set; } = string.Empty;

    public string MemberName { get; set; } = string.Empty;

    public string BookUniqueId { get; set; } = string.Empty;

    public string BookTitle { get; set; } = string.Empty;

    public decimal FineAmount { get; set; }

    public bool IsPaid { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? PaidOn { get; set; }
}