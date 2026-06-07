using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Store.Abstractions;
using LibraryManagementSystem.Store.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using LibraryManagementSystem.Common.Constants;

namespace LibraryManagementSystem.Store.Implementations;

public class CategoryStore : ICategoryStore
{
    private readonly LibraryDbContext _context;

    public CategoryStore(
        LibraryDbContext context)
    {
        _context = context;
    }

    private DataTable BuildCategoryDataTable(
    List<CreateCategoryRequestDto> categories)
    {
        var table = new DataTable();

        table.Columns.Add(
            "CategoryName",
            typeof(string));

        foreach (var category in categories)
        {
            table.Rows.Add(
                category.CategoryName);
        }

        return table;
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

    public async Task BulkInsertCategoriesAsync(
    List<CreateCategoryRequestDto> categories,
    string createdBy)
    {
        try
        {
            var table =
                BuildCategoryDataTable(categories);

            var categoriesParameter =
                new SqlParameter(
                    "@Categories",
                    table)
                {
                    SqlDbType =
                        SqlDbType.Structured,

                    TypeName =
                        "Category_Type"
                };

            var createdByParameter =
                new SqlParameter(
                    "@CreatedBy",
                    createdBy);

            await _context.Database
                .ExecuteSqlRawAsync(
                    $"EXEC {SqlConstants.UspBulkInsertCategories}\n                    @Categories,\n                    @CreatedBy",
                    categoriesParameter,
                    createdByParameter);
        }
        catch (Exception ex)
        {
            throw new Exception(
                "Error occurred while bulk inserting categories.",
                ex);
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