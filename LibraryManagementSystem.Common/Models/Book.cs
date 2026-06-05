using LibraryManagementSystem.Common.Models;

public class Book : BaseModel
{
    public int BookId { get; set; }

    public int CategoryId { get; set; }

    public int AuthorId { get; set; }

    public string? ISBN { get; set; }

    public string Title { get; set; } = string.Empty;

    public int TotalCopies { get; set; }

    public int AvailableCopies { get; set; }

    public int? PublishedYear { get; set; }

    // For API response only
    public string AuthorName { get; set; } = string.Empty;

    public string CategoryName { get; set; } = string.Empty;
}