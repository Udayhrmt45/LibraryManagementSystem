using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;

namespace LibraryManagementSystem.Service.Abstractions;

public interface ICategoryService
{
    Task<CategoryResponseDto>
        CreateCategoryAsync(
            CreateCategoryRequestDto request);

    Task<List<CategoryResponseDto>>
        GetAllCategoriesAsync();

    Task<PagedResponseDto<CategoryResponseDto>>
        GetCategoriesPagedAsync(
            PaginationRequestDto request);

    Task<CategoryResponseDto>
        GetCategoryByUniqueIdAsync(
            string uniqueId);

    Task BulkInsertCategoriesAsync(
    BulkInsertCategoriesRequestDto request);

    Task UpdateCategoryAsync(
        string uniqueId,
        UpdateCategoryRequestDto request);

    Task DeleteCategoryAsync(
        string uniqueId);
}