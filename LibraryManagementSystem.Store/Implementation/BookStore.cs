using LibraryManagementSystem.Common.DTOs.Request;
using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Store.Abstractions;
using LibraryManagementSystem.Store.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using LibraryManagementSystem.Common.DTOs.Requests;

namespace LibraryManagementSystem.Store.Implementations;

public class BookStore : IBookStore
{
    private readonly LibraryDbContext _context;

    public BookStore(
        LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<Book> CreateBookAsync(
        Book book)
    {
        try
        {
            await _context.Books.AddAsync(book);

            await _context.SaveChangesAsync();

            return book;
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(CreateBookAsync)}: {ex.Message}");
        }
    }

    public async Task<List<Book>>
        GetAllBooksAsync()
    {
        try
        {
            return await (
                from b in _context.Books
                join a in _context.Authors
                    on b.AuthorId equals a.AuthorId
                join c in _context.Categories
                    on b.CategoryId equals c.CategoryId
                where b.IsActive
                select new Book
                {
                    BookId = b.BookId,
                    UniqueId = b.UniqueId,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    TotalCopies = b.TotalCopies,
                    AvailableCopies = b.AvailableCopies,
                    PublishedYear = b.PublishedYear,
                    AuthorName = a.AuthorName,
                    CategoryName = c.CategoryName
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetAllBooksAsync)}: {ex.Message}");
        }
    }

    public async Task<(List<Book> Books, int TotalRecords)>
    GetBooksPagedAsync(
        BookFilterRequestDto request)
    {
        try
        {
            var query =
                from b in _context.Books
                join a in _context.Authors
                    on b.AuthorId equals a.AuthorId
                join c in _context.Categories
                    on b.CategoryId equals c.CategoryId
                where b.IsActive
                select new Book
                {
                    BookId = b.BookId,
                    UniqueId = b.UniqueId,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    TotalCopies = b.TotalCopies,
                    AvailableCopies = b.AvailableCopies,
                    PublishedYear = b.PublishedYear,
                    AuthorName = a.AuthorName,
                    CategoryName = c.CategoryName,
                    AuthorId = b.AuthorId,
                    CategoryId = b.CategoryId
                };

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query =
                    query.Where(x =>
                        x.Title.Contains(request.Title));
            }

            if (!string.IsNullOrWhiteSpace(request.ISBN))
            {
                query =
                    query.Where(x =>
                        x.ISBN == request.ISBN);
            }

            if (request.PublishedYear.HasValue)
            {
                query =
                    query.Where(x =>
                        x.PublishedYear ==
                        request.PublishedYear);
            }

            if (!string.IsNullOrWhiteSpace(
                    request.AuthorUniqueId))
            {
                var author =
                    await _context.Authors
                        .FirstOrDefaultAsync(x =>
                            x.UniqueId ==
                            request.AuthorUniqueId);

                if (author != null)
                {
                    query =
                        query.Where(x =>
                            x.AuthorId ==
                            author.AuthorId);
                }
            }

            if (!string.IsNullOrWhiteSpace(
                    request.CategoryUniqueId))
            {
                var category =
                    await _context.Categories
                        .FirstOrDefaultAsync(x =>
                            x.UniqueId ==
                            request.CategoryUniqueId);

                if (category != null)
                {
                    query =
                        query.Where(x =>
                            x.CategoryId ==
                            category.CategoryId);
                }
            }

            var totalRecords =
                await query.CountAsync();

            var books =
                await query
                    .OrderBy(x => x.BookId)
                    .Skip(
                        (request.PageNumber - 1)
                        * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

            return (books, totalRecords);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetBooksPagedAsync)}: {ex.Message}");
        }
    }

    public async Task BulkInsertBooksAsync(
    List<BulkBookRequestDto> books,
    string createdBy)
    {
        try
        {
            var table = new DataTable();

            table.Columns.Add(
                "AuthorUniqueId",
                typeof(string));

            table.Columns.Add(
                "CategoryUniqueId",
                typeof(string));

            table.Columns.Add(
                "ISBN",
                typeof(string));

            table.Columns.Add(
                "Title",
                typeof(string));

            table.Columns.Add(
                "TotalCopies",
                typeof(int));

            table.Columns.Add(
                "PublishedYear",
                typeof(int));

            foreach (var book in books)
            {
                table.Rows.Add(
                    book.AuthorUniqueId,
                    book.CategoryUniqueId,
                    book.ISBN,
                    book.Title,
                    book.TotalCopies,
                    book.PublishedYear);
            }

            var connection =
                (SqlConnection)
                _context.Database.GetDbConnection();

            if (connection.State !=
                ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command =
                new SqlCommand(
                    "usp_BulkInsertBooks",
                    connection);

            command.CommandType =
                CommandType.StoredProcedure;

            command.Parameters.Add(
                new SqlParameter(
                    "@Books",
                    SqlDbType.Structured)
                {
                    TypeName = "dbo.Book_Type",
                    Value = table
                });

            command.Parameters.Add(
                new SqlParameter(
                    "@CreatedBy",
                    createdBy));

            await command.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(BulkInsertBooksAsync)}: {ex.Message}");
        }
    }

    public async Task<Book?> GetBookByUniqueIdAsync(
        string uniqueId)
    {
        try
        {
            return await _context.Books
                .FirstOrDefaultAsync(x =>
                    x.UniqueId == uniqueId &&
                    x.IsActive);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetBookByUniqueIdAsync)}: {ex.Message}");
        }
    }

    public async Task<Book?> GetBookByIsbnAsync(
        string isbn)
    {
        try
        {
            return await _context.Books
                .FirstOrDefaultAsync(x =>
                    x.ISBN == isbn &&
                    x.IsActive);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetBookByIsbnAsync)}: {ex.Message}");
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

    public async Task<Category?> GetCategoryByUniqueIdAsync(
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

    public async Task UpdateBookAsync(
        Book book)
    {
        try
        {
            _context.Books.Update(book);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(UpdateBookAsync)}: {ex.Message}");
        }
    }

    public async Task SoftDeleteBookAsync(
        Book book)
    {
        try
        {
            book.IsActive = false;

            book.UpdatedOn = DateTime.UtcNow;

            _context.Books.Update(book);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(SoftDeleteBookAsync)}: {ex.Message}");
        }
    }
}