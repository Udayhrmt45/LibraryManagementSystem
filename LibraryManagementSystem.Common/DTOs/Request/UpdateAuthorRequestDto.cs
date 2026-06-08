using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Common.DTOs.Requests;

public class UpdateAuthorRequestDto
{
    [Required(ErrorMessage = "Author name is required.")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Author name must be between 2 and 200 characters.")]
    public string AuthorName { get; set; } = string.Empty;
}