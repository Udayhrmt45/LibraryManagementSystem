using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.Common.DTOs.Requests;

public class BulkInsertAuthorsRequestDto
{
    [Required]
    [MinLength(1, ErrorMessage = "At least one item is required.")]
    public List<CreateAuthorRequestDto> Authors { get; set; }
        = new();
}