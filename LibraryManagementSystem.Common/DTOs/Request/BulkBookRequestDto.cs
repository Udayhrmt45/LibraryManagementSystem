namespace LibraryManagementSystem.Common.DTOs.Requests;

public class BulkBookRequestDto
{
    public string AuthorUniqueId { get; set; } = string.Empty;

    public string CategoryUniqueId { get; set; } = string.Empty;

    public string? ISBN { get; set; }

    public string Title { get; set; } = string.Empty;

    public int TotalCopies { get; set; }

    public int PublishedYear { get; set; }
}