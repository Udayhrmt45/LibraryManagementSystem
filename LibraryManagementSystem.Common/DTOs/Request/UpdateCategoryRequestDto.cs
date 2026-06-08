using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Common.DTOs.Requests;

public class UpdateCategoryRequestDto
{
    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 200 characters.")]
    public string CategoryName { get; set; } = string.Empty;
}