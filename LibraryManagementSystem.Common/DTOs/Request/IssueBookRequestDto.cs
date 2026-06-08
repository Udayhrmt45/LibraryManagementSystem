using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Common.DTOs.Requests;

public class IssueBookRequestDto
{
    [Required(ErrorMessage = "Unique identifier is required.")]
    [StringLength(32)]
    public string MemberUniqueId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Unique identifier is required.")]
    [StringLength(32)]
    public string BookUniqueId { get; set; } = string.Empty;

    [Range(1, 365, ErrorMessage = "Borrow days must be between 1 and 365.")]
    public int BorrowDays { get; set; } = 14;
}