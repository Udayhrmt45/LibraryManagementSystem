using LibraryManagementSystem.Common.DTOs.Requests;

namespace LibraryManagementSystem.Common.DTOs.Request;

public class BookFilterRequestDto : PaginationRequestDto
{
    public string? Title { get; set; }

    public string? ISBN { get; set; }

    public string? AuthorUniqueId { get; set; }

    public string? CategoryUniqueId { get; set; }

    public int? PublishedYear { get; set; }
}