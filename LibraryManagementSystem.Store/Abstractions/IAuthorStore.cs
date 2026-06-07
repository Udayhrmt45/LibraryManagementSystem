using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.Models;

public interface IAuthorStore
{
    Task<Author> CreateAuthorAsync(Author author);

    Task<List<Author>> GetAllAuthorsAsync();

    Task<(List<Author> Authors, int TotalRecords)>
        GetAuthorsPagedAsync(
            int pageNumber,
            int pageSize);

    Task<Author?> GetAuthorByUniqueIdAsync(
        string uniqueId);

    Task BulkInsertAuthorsAsync(
    List<CreateAuthorRequestDto> authors,
    string createdBy);

    Task UpdateAuthorAsync(Author author);

    Task SoftDeleteAuthorAsync(Author author);
}