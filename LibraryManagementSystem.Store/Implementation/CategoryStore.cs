using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Store.Abstractions;
using LibraryManagementSystem.Store.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Store.Implementations;

public class CategoryStore : ICategoryStore
{
    private readonly LibraryDbContext _context;

    public CategoryStore(
        LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<Category> CreateCategoryAsync(
        Category category)
    {
        try
        {
            await _context.Categories.AddAsync(category);

            await _context.SaveChangesAsync();

            return category;
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(CreateCategoryAsync)}: {ex.Message}");
        }
    }

    public async Task<List<Category>>
        GetAllCategoriesAsync()
    {
        try
        {
            return await _context.Categories
                .Where(x => x.IsActive)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetAllCategoriesAsync)}: {ex.Message}");
        }
    }

    public async Task<(List<Category> Categories,
        int TotalRecords)>
        GetCategoriesPagedAsync(
            int pageNumber,
            int pageSize)
    {
        try
        {
            var query =
                _context.Categories
                    .Where(x => x.IsActive);

            var totalRecords =
                await query.CountAsync();

            var categories =
                await query
                    .OrderBy(x => x.CategoryId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            return (categories, totalRecords);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetCategoriesPagedAsync)}: {ex.Message}");
        }
    }

    public async Task<Category?>
        GetCategoryByUniqueIdAsync(
            string uniqueId)
    {
        try
        {
            return await _context.Categories
                .FirstOrDefaultAsync(x =>
                    x.UniqueId == uniqueId &&
                    x.IsActive);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetCategoryByUniqueIdAsync)}: {ex.Message}");
        }
    }

    public async Task UpdateCategoryAsync(
        Category category)
    {
        try
        {
            _context.Categories.Update(category);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(UpdateCategoryAsync)}: {ex.Message}");
        }
    }

    public async Task SoftDeleteCategoryAsync(
        Category category)
    {
        try
        {
            category.IsActive = false;

            category.UpdatedOn = DateTime.UtcNow;

            _context.Categories.Update(category);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(SoftDeleteCategoryAsync)}: {ex.Message}");
        }
    }
}