namespace LibraryManagementSystem.Common.Models;

public class Author : BaseModel
{
    public int AuthorId { get; set; }

    public string AuthorName { get; set; } = string.Empty;
}