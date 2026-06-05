using LibraryManagementSystem.Common.DTOs.Request;
using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;

namespace LibraryManagementSystem.Service.Abstractions;

public interface IBookService
{
    Task<BookResponseDto> CreateBookAsync(
        CreateBookRequestDto request);

    Task<List<BookResponseDto>> GetAllBooksAsync();

    Task<PagedResponseDto<BookResponseDto>>
    GetBooksPagedAsync(
        BookFilterRequestDto request);

    Task<BookResponseDto> GetBookByUniqueIdAsync(
        string uniqueId);

    Task BulkInsertBooksAsync(List<BulkBookRequestDto> request);

    Task UpdateBookAsync(
        string uniqueId,
        UpdateBookRequestDto request);

    Task DeleteBookAsync(
        string uniqueId);
}