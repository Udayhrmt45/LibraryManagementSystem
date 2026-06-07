using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.Models;

namespace LibraryManagementSystem.Store.Abstractions;

public interface ICategoryStore
{
    Task<Category> CreateCategoryAsync(
        Category category);

    Task<List<Category>> GetAllCategoriesAsync();

    Task<(List<Category> Categories, int TotalRecords)>
        GetCategoriesPagedAsync(
            int pageNumber,
            int pageSize);

    Task<Category?> GetCategoryByUniqueIdAsync(
        string uniqueId);

    Task BulkInsertCategoriesAsync(
    List<CreateCategoryRequestDto> categories,
    string createdBy);

    Task UpdateCategoryAsync(
        Category category);

    Task SoftDeleteCategoryAsync(
        Category category);
}