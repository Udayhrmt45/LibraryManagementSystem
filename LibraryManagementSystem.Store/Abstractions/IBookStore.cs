using LibraryManagementSystem.Common.DTOs.Request;
using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.Models;

namespace LibraryManagementSystem.Store.Abstractions;

public interface IBookStore
{
    Task<Book> CreateBookAsync(Book book);

    Task<List<Book>> GetAllBooksAsync();

    Task<(List<Book> Books, int TotalRecords)>
    GetBooksPagedAsync(
        BookFilterRequestDto request);

    Task<Book?> GetBookByUniqueIdAsync(
        string uniqueId);

    Task<Book?> GetBookByIsbnAsync(
        string isbn);

    Task<Author?> GetAuthorByUniqueIdAsync(
        string uniqueId);

    Task<Category?> GetCategoryByUniqueIdAsync(
        string uniqueId);

    Task BulkInsertBooksAsync(
    List<BulkBookRequestDto> books,
    string createdBy);

    Task UpdateBookAsync(Book book);

    Task SoftDeleteBookAsync(Book book);
}