using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Common.DTOs.Requests;

public class ReturnBookRequestDto
{
    [Required(ErrorMessage = "Unique identifier is required.")]
    [StringLength(32)]
    public string IssueUniqueId { get; set; } = string.Empty;
}