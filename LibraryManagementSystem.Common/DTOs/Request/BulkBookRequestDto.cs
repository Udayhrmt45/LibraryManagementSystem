using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Common.DTOs.Requests;

public class BulkBookRequestDto
{
    [Required(ErrorMessage = "Unique identifier is required.")]
    [StringLength(32)]
    public string AuthorUniqueId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Unique identifier is required.")]
    [StringLength(32)]
    public string CategoryUniqueId { get; set; } = string.Empty;

    public string? ISBN { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 200 characters.")]
    public string Title { get; set; } = string.Empty;

    [Range(1, 1000, ErrorMessage = "Total copies must be greater than zero.")]
    public int TotalCopies { get; set; }

    [Range(1000, 9999, ErrorMessage = "Invalid published year.")]
    public int PublishedYear { get; set; }
}