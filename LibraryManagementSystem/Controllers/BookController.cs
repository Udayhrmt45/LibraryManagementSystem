using LibraryManagementSystem.Common.DTOs.Request;
using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers;

[Authorize(Roles = "Admin,Librarian")]
[ApiController]
[Route("api/[controller]/[action]")]
/// <summary>
/// Controller for managing books in the library.
/// </summary>
/// <remarks>
/// Exposes endpoints to create, retrieve, update, delete and bulk-insert books. Requires
/// the Admin or Librarian role to access these endpoints.
/// </remarks>
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(
        IBookService bookService)
    {
        _bookService = bookService;
    }

    /// <summary>
    /// Creates a new book record.
    /// </summary>
    /// <remarks>
    /// Creates a book using the provided CreateBookRequestDto and returns the result from the service.
    /// </remarks>
    /// <param name="request">The book creation request containing title, author, category and other metadata.</param>
    /// <returns>The created book information or service response.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost]
    public async Task<IActionResult> CreateBookAsync(
        CreateBookRequestDto request)
    {
        var result =
            await _bookService
                .CreateBookAsync(request);

        return Ok(result);
    }

    /// <summary>
    /// Retrieves all books.
    /// </summary>
    /// <remarks>
    /// Returns a list of all books in the library.
    /// </remarks>
    /// <returns>A collection of all books.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    public async Task<IActionResult> GetAllBooksAsync()
    {
        return Ok(
            await _bookService
                .GetAllBooksAsync());
    }

    /// <summary>
    /// Retrieves books with paging and optional filters.
    /// </summary>
    /// <remarks>
    /// Supports filtering and pagination; response contains page number, page size and total records.
    /// </remarks>
    /// <param name="request">Filtering and pagination parameters.</param>
    /// <returns>Paged list of books and pagination metadata.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    public async Task<IActionResult> GetBooksPagedAsync(
    [FromQuery]
    BookFilterRequestDto request)
    {
        return Ok(
            await _bookService
                .GetBooksPagedAsync(request));
    }

    /// <summary>
    /// Retrieves a book by unique identifier.
    /// </summary>
    /// <remarks>
    /// Returns detailed information for a specific book identified by uniqueId.
    /// </remarks>
    /// <param name="uniqueId">The unique identifier of the book to retrieve.</param>
    /// <returns>The book details.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("{uniqueId}")]
    public async Task<IActionResult> GetBookByUniqueIdAsync(string uniqueId)
    {
        return Ok(
            await _bookService
                .GetBookByUniqueIdAsync(
                    uniqueId));
    }

    /// <summary>
    /// Bulk inserts books.
    /// </summary>
    /// <remarks>
    /// Performs bulk insertion of books using a list of BulkBookRequestDto. Intended for large uploads.
    /// </remarks>
    /// <param name="request">List of books to insert in bulk.</param>
    /// <returns>Confirmation message on successful upload.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost]
    public async Task<IActionResult> BulkInsertBooksAsync(List<BulkBookRequestDto> request)
    {
        await _bookService
            .BulkInsertBooksAsync(request);

        return Ok(
            "Books uploaded successfully");
    }

    /// <summary>
    /// Updates an existing book.
    /// </summary>
    /// <remarks>
    /// Updates the book identified by uniqueId with provided data.
    /// </remarks>
    /// <param name="uniqueId">The unique identifier of the book to update.</param>
    /// <param name="request">The update request containing modified book fields.</param>
    /// <returns>Confirmation message on successful update.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPut("{uniqueId}")]
    public async Task<IActionResult> UpdateBookAsync(string uniqueId,UpdateBookRequestDto request)
    {
        await _bookService
            .UpdateBookAsync(
                uniqueId,
                request);

        return Ok(
            "Book updated successfully");
    }

    /// <summary>
    /// Deletes a book.
    /// </summary>
    /// <remarks>
    /// Deletes the book identified by uniqueId from the system.
    /// </remarks>
    /// <param name="uniqueId">The unique identifier of the book to delete.</param>
    /// <returns>Confirmation message on successful deletion.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpDelete("{uniqueId}")]
    public async Task<IActionResult> DeleteBookAsync(string uniqueId)
    {
        await _bookService
            .DeleteBookAsync(
                uniqueId);

        return Ok(
            "Book deleted successfully");
    }
}