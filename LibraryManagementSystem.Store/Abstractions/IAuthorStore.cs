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

    Task UpdateAuthorAsync(Author author);

    Task SoftDeleteAuthorAsync(Author author);
}