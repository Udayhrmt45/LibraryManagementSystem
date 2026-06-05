using LibraryManagementSystem.Common.DTOs.Request;
using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;
using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Service.Abstractions;
using LibraryManagementSystem.Store.Abstractions;

namespace LibraryManagementSystem.Service.Implementations;

public class BookService : IBookService
{
    private readonly IBookStore _bookStore;

    public BookService(
        IBookStore bookStore)
    {
        _bookStore = bookStore;
    }

    public async Task<BookResponseDto> CreateBookAsync(
        CreateBookRequestDto request)
    {
        try
        {
            var author =
                await _bookStore
                    .GetAuthorByUniqueIdAsync(
                        request.AuthorUniqueId);

            if (author == null)
            {
                throw new Exception(
                    "Invalid Author");
            }

            var category =
                await _bookStore
                    .GetCategoryByUniqueIdAsync(
                        request.CategoryUniqueId);

            if (category == null)
            {
                throw new Exception(
                    "Invalid Category");
            }

            if (!string.IsNullOrWhiteSpace(
                    request.ISBN))
            {
                var existingBook =
                    await _bookStore
                        .GetBookByIsbnAsync(
                            request.ISBN);

                if (existingBook != null)
                {
                    throw new Exception(
                        "ISBN already exists");
                }
            }

            var book = new Book
            {
                AuthorId = author.AuthorId,

                CategoryId = category.CategoryId,

                Title = request.Title,

                ISBN = request.ISBN,

                TotalCopies = request.TotalCopies,

                AvailableCopies =
                    request.TotalCopies,

                PublishedYear =
                    request.PublishedYear,

                IsActive = true,

                CreatedOn = DateTime.UtcNow
            };

            var createdBook =
                await _bookStore
                    .CreateBookAsync(book);

            return new BookResponseDto
            {
                UniqueId = createdBook.UniqueId,

                Title = createdBook.Title,

                ISBN = createdBook.ISBN,

                AuthorName = author.AuthorName,

                CategoryName = category.CategoryName,

                TotalCopies = createdBook.TotalCopies,

                AvailableCopies =
                    createdBook.AvailableCopies,

                PublishedYear =
                    createdBook.PublishedYear
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(CreateBookAsync)}: {ex.Message}");
        }
    }

    public async Task<List<BookResponseDto>>
        GetAllBooksAsync()
    {
        try
        {
            var books =
                await _bookStore
                    .GetAllBooksAsync();

            return books.Select(book =>
                new BookResponseDto
                {
                    UniqueId = book.UniqueId,

                    Title = book.Title,

                    ISBN = book.ISBN,

                    AuthorName = book.AuthorName,

                    CategoryName = book.CategoryName,

                    TotalCopies = book.TotalCopies,

                    AvailableCopies =
                        book.AvailableCopies,

                    PublishedYear =
                        book.PublishedYear
                }).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetAllBooksAsync)}: {ex.Message}");
        }
    }

    public async Task<PagedResponseDto<BookResponseDto>>
    GetBooksPagedAsync(
        BookFilterRequestDto request)
    {
        try
        {
            var result =
                await _bookStore
                    .GetBooksPagedAsync(request);

            return new PagedResponseDto<BookResponseDto>
            {
                PageNumber = request.PageNumber,

                PageSize = request.PageSize,

                TotalRecords = result.TotalRecords,

                TotalPages =
                    (int)Math.Ceiling(
                        (double)result.TotalRecords /
                        request.PageSize),

                Data = result.Books
                    .Select(book =>
                        new BookResponseDto
                        {
                            UniqueId = book.UniqueId,
                            Title = book.Title,
                            ISBN = book.ISBN,
                            AuthorName = book.AuthorName,
                            CategoryName = book.CategoryName,
                            TotalCopies = book.TotalCopies,
                            AvailableCopies = book.AvailableCopies,
                            PublishedYear = book.PublishedYear
                        })
                    .ToList()
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetBooksPagedAsync)}: {ex.Message}");
        }
    }

    public async Task<BookResponseDto>
        GetBookByUniqueIdAsync(
            string uniqueId)
    {
        try
        {
            var book =
                await _bookStore
                    .GetBookByUniqueIdAsync(
                        uniqueId);

            if (book == null)
            {
                throw new Exception(
                    "Book not found");
            }

            return new BookResponseDto
            {
                UniqueId = book.UniqueId,

                Title = book.Title,

                ISBN = book.ISBN,

                TotalCopies = book.TotalCopies,

                AvailableCopies =
                    book.AvailableCopies,

                PublishedYear =
                    book.PublishedYear
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetBookByUniqueIdAsync)}: {ex.Message}");
        }
    }

    public async Task BulkInsertBooksAsync(List<BulkBookRequestDto> request)
    {
        try
        {
            if (request == null || !request.Any())
            {
                throw new Exception("No records supplied");
            }

            await _bookStore
                .BulkInsertBooksAsync(
                    request,
                    "SYSTEM");
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(BulkInsertBooksAsync)}: {ex.Message}");
        }
    }

    public async Task UpdateBookAsync(
        string uniqueId,
        UpdateBookRequestDto request)
    {
        try
        {
            var book =
                await _bookStore
                    .GetBookByUniqueIdAsync(
                        uniqueId);

            if (book == null)
            {
                throw new Exception(
                    "Book not found");
            }

            var author =
                await _bookStore
                    .GetAuthorByUniqueIdAsync(
                        request.AuthorUniqueId);

            if (author == null)
            {
                throw new Exception(
                    "Invalid Author");
            }

            var category =
                await _bookStore
                    .GetCategoryByUniqueIdAsync(
                        request.CategoryUniqueId);

            if (category == null)
            {
                throw new Exception(
                    "Invalid Category");
            }

            book.AuthorId = author.AuthorId;

            book.CategoryId = category.CategoryId;

            book.Title = request.Title;

            book.ISBN = request.ISBN;

            book.TotalCopies =
                request.TotalCopies;

            book.PublishedYear =
                request.PublishedYear;

            book.UpdatedOn =
                DateTime.UtcNow;

            await _bookStore
                .UpdateBookAsync(book);
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(UpdateBookAsync)}: {ex.Message}");
        }
    }

    public async Task DeleteBookAsync(
        string uniqueId)
    {
        try
        {
            var book =
                await _bookStore
                    .GetBookByUniqueIdAsync(
                        uniqueId);

            if (book == null)
            {
                throw new Exception(
                    "Book not found");
            }

            await _bookStore
                .SoftDeleteBookAsync(book);
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(DeleteBookAsync)}: {ex.Message}");
        }
    }
}