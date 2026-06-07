using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;

namespace LibraryManagementSystem.Service.Abstractions;

public interface IAuthorService
{
    Task<AuthorResponseDto> CreateAuthorAsync(
        CreateAuthorRequestDto request);

    Task<List<AuthorResponseDto>> GetAllAuthorsAsync();

    Task<AuthorResponseDto> GetAuthorByUniqueIdAsync(
        string uniqueId);

    Task<PagedResponseDto<AuthorResponseDto>> GetAuthorsPagedAsync(PaginationRequestDto request);

    Task BulkInsertAuthorsAsync(
    BulkInsertAuthorsRequestDto request);

    Task UpdateAuthorAsync(
        string uniqueId,
        UpdateAuthorRequestDto request);

    Task DeleteAuthorAsync(
        string uniqueId);
}