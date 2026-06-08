using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.Common.DTOs.Requests;

public class BulkInsertCategoriesRequestDto
{
    [Required]
    [MinLength(1, ErrorMessage = "At least one item is required.")]
    public List<CreateCategoryRequestDto> Categories {get; set;} = new();
}