using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;
using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Service.Abstractions;
using LibraryManagementSystem.Store.Abstractions;


namespace LibraryManagementSystem.Service.Implementations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryStore _categoryStore;

    public CategoryService(
        ICategoryStore categoryStore)
    {
        _categoryStore = categoryStore;
    }

    public async Task<CategoryResponseDto> CreateCategoryAsync(
        CreateCategoryRequestDto request)
    {
        try
        {
            var category = new Category
            {
                CategoryName = request.CategoryName,

                IsActive = true,

                CreatedOn = DateTime.UtcNow
            };

            var createdcategory =
                await _categoryStore.CreateCategoryAsync(
                    category);

            return new CategoryResponseDto
            {
                UniqueId = createdcategory.UniqueId,

                CategoryName = createdcategory.CategoryName
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(CreateCategoryAsync)}: {ex.Message}");
        }
    }

    public async Task<List<CategoryResponseDto>> GetAllCategoriesAsync()
    {
        try
        {
            var categorys =
                await _categoryStore.GetAllCategoriesAsync();

            return categorys.Select(category =>
                new CategoryResponseDto
                {
                    UniqueId = category.UniqueId,

                    CategoryName = category.CategoryName
                }).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetAllCategoriesAsync)}: {ex.Message}");
        }
    }

    public async Task<CategoryResponseDto> GetCategoryByUniqueIdAsync(string uniqueId)
    {
        try
        {
            var category =
                await _categoryStore
                    .GetCategoryByUniqueIdAsync(
                        uniqueId);

            if (category == null)
            {
                throw new Exception(
                    "category not found");
            }

            return new CategoryResponseDto
            {
                UniqueId = category.UniqueId,

                CategoryName = category.CategoryName
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetCategoryByUniqueIdAsync)}: {ex.Message}");
        }
    }


    public async Task<PagedResponseDto<CategoryResponseDto>> GetCategoriesPagedAsync(PaginationRequestDto request)
    {
        try
        {
            var result =
                await _categoryStore.GetCategoriesPagedAsync(
                    request.PageNumber,
                    request.PageSize);

            return new PagedResponseDto<CategoryResponseDto>
            {
                PageNumber = request.PageNumber,

                PageSize = request.PageSize,

                TotalRecords = result.TotalRecords,

                TotalPages =
                    (int)Math.Ceiling(
                        (double)result.TotalRecords /
                        request.PageSize),

                Data = result.Categories
                    .Select(category =>
                        new CategoryResponseDto
                        {
                            UniqueId = category.UniqueId,

                            CategoryName = category.CategoryName
                        })
                    .ToList()
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetCategoriesPagedAsync)}: {ex.Message}");
        }
    }

    public async Task BulkInsertCategoriesAsync(
    BulkInsertCategoriesRequestDto request)
    {
        await _categoryStore
            .BulkInsertCategoriesAsync(
                request.Categories,
                "System");
    }

    public async Task UpdateCategoryAsync(
        string uniqueId,
        UpdateCategoryRequestDto request)
    {
        try
        {
            var category =
                await _categoryStore
                    .GetCategoryByUniqueIdAsync(
                        uniqueId);

            if (category == null)
            {
                throw new Exception(
                    "category not found");
            }

            category.CategoryName =
                request.CategoryName;

            category.UpdatedOn =
                DateTime.UtcNow;

            await _categoryStore
                .UpdateCategoryAsync(category);
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(UpdateCategoryAsync)}: {ex.Message}");
        }
    }

    public async Task DeleteCategoryAsync(
        string uniqueId)
    {
        try
        {
            var category =
                await _categoryStore
                    .GetCategoryByUniqueIdAsync(
                        uniqueId);

            if (category == null)
            {
                throw new Exception(
                    "category not found");
            }

            await _categoryStore
                .SoftDeleteCategoryAsync(category);
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(DeleteCategoryAsync)}: {ex.Message}");
        }
    }
}