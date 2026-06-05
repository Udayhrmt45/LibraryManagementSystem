namespace LibraryManagementSystem.Common.DTOs.Requests;

public class CreateBookRequestDto
{
    public string AuthorUniqueId { get; set; } = string.Empty;

    public string CategoryUniqueId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string? ISBN { get; set; }

    public int TotalCopies { get; set; }

    public int PublishedYear { get; set; }
}