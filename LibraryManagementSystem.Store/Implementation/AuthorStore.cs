using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Store.Abstractions;
using LibraryManagementSystem.Store.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using LibraryManagementSystem.Common.Constants;

namespace LibraryManagementSystem.Store.Implementations;

public class AuthorStore : IAuthorStore
{
    private readonly LibraryDbContext _context;

    public AuthorStore(
        LibraryDbContext context)
    {
        _context = context;
    }

    private DataTable BuildAuthorDataTable(
    List<CreateAuthorRequestDto> authors)
    {
        var table = new DataTable();

        table.Columns.Add(
            "AuthorName",
            typeof(string));

        foreach (var author in authors)
        {
            table.Rows.Add(
                author.AuthorName);
        }

        return table;
    }

    public async Task<Author> CreateAuthorAsync(
        Author author)
    {
        try
        {
            await _context.Authors.AddAsync(author);

            await _context.SaveChangesAsync();

            return author;
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(CreateAuthorAsync)}: {ex.Message}");
        }
    }

    public async Task<List<Author>> GetAllAuthorsAsync()
    {
        try
        {
            return await _context.Authors
                .Where(x => x.IsActive)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetAllAuthorsAsync)}: {ex.Message}");
        }
    }

    public async Task<Author?> GetAuthorByUniqueIdAsync(
        string uniqueId)
    {
        try
        {
            return await _context.Authors
                .FirstOrDefaultAsync(x =>
                    x.UniqueId == uniqueId &&
                    x.IsActive);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetAuthorByUniqueIdAsync)}: {ex.Message}");
        }
    }

    public async Task<(List<Author> Authors, int TotalRecords)>
    GetAuthorsPagedAsync(
        int pageNumber,
        int pageSize)
    {
        try
        {
            var query =
                _context.Authors
                    .Where(x => x.IsActive);

            var totalRecords =
                await query.CountAsync();

            var authors =
                await query
                    .OrderBy(x => x.AuthorId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            return (authors, totalRecords);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetAuthorsPagedAsync)}: {ex.Message}");
        }
    }

    public async Task BulkInsertAuthorsAsync(
    List<CreateAuthorRequestDto> authors,
    string createdBy)
    {
        try
        {
            var table =
                BuildAuthorDataTable(authors);

            var authorsParameter =
                new SqlParameter(
                    "@Authors",
                    table)
                {
                    SqlDbType =
                        SqlDbType.Structured,

                    TypeName =
                        "Author_Type"
                };

            var createdByParameter =
                new SqlParameter(
                    "@CreatedBy",
                    createdBy);

            await _context.Database.ExecuteSqlRawAsync(
                $"EXEC {SqlConstants.UspBulkInsertAuthors}\n                @Authors,\n                @CreatedBy",
                authorsParameter,
                createdByParameter);
        }
        catch (Exception ex)
        {
            throw new Exception(
                "Error occurred while bulk inserting authors.",
                ex);
        }
    }

    public async Task UpdateAuthorAsync(
        Author author)
    {
        try
        {
            _context.Authors.Update(author);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(UpdateAuthorAsync)}: {ex.Message}");
        }
    }

    public async Task SoftDeleteAuthorAsync(
        Author author)
    {
        try
        {
            author.IsActive = false;

            author.UpdatedOn = DateTime.UtcNow;

            _context.Authors.Update(author);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(SoftDeleteAuthorAsync)}: {ex.Message}");
        }
    }
}