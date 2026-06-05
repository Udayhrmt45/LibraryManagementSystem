namespace LibraryManagementSystem.Common.DTOs.Responses;

public class BookResponseDto
{
    public string UniqueId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string? ISBN { get; set; }

    public string AuthorName { get; set; } = string.Empty;

    public string CategoryName { get; set; } = string.Empty;

    public int TotalCopies { get; set; }

    public int AvailableCopies { get; set; }

    public int? PublishedYear { get; set; }
}