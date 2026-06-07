using LibraryManagementSystem.Common.DTOs.Requests;

public class BulkInsertAuthorsRequestDto
{
    public List<CreateAuthorRequestDto> Authors { get; set; }
        = new();
}