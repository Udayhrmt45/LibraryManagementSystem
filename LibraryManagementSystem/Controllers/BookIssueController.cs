using LibraryManagementSystem.Common;
using LibraryManagementSystem.Common.Constants;
using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;
using LibraryManagementSystem.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers;

[Authorize(Roles = AppConstants.AdminRole + "," + AppConstants.LibrarianRole)]
[ApiController]
[Route("api/[controller]/[action]")]
/// <summary>
/// Controller for issuing and returning books.
/// </summary>
/// <remarks>
/// Handles book issue, return and history retrieval operations. Requires Admin or Librarian role.
/// </remarks>
public class BookIssueController : ControllerBase
{
    private readonly IBookIssueService _bookIssueService;

    public BookIssueController(IBookIssueService bookIssueService)
    {
        _bookIssueService = bookIssueService;
    }

    /// <summary>
    /// Issues a book to a member.
    /// </summary>
    /// <remarks>
    /// Creates a book issue record for the specified member and book, returning the issue details.
    /// </remarks>
    /// <param name="request">Details required to issue the book such as member id, book id and due date.</param>
    /// <returns>A CommonResponse wrapping the BookIssueResponseDto.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost]
    public async Task<IActionResult> IssueBookAsync(
        IssueBookRequestDto request)
    { 
        await _bookIssueService.IssueBookAsync(request, User.FindFirst(ClaimTypes.Role)?.Value ?? "System");

        return Ok(
            new CommonResponse<BookIssueResponseDto>
            {
                Success = true,
                Message = AppConstants.BookIssued
            });
    }

    /// <summary>
    /// Returns a previously issued book.
    /// </summary>
    /// <remarks>
    /// Processes return of a book and updates the issue record accordingly.
    /// </remarks>
    /// <param name="request">Details required to return the book such as issue id and return date.</param>
    /// <returns>A CommonResponse indicating successful return.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost]
    public async Task<IActionResult> ReturnBookAsync(ReturnBookRequestDto request)
    {
        await _bookIssueService
            .ReturnBookAsync(request);

        return Ok(
            new CommonResponse<string>
            {
                Success = true,
                Message = AppConstants.BookReturned
            });
    }

    /// <summary>
    /// Retrieves all book issue records.
    /// </summary>
    /// <remarks>
    /// Returns a list of all issued book records for administrative review.
    /// </remarks>
    /// <returns>A CommonResponse wrapping a list of BookIssueResponseDto objects.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    public async Task<IActionResult> GetAllIssuesAsync()
    {
        var result =
            await _bookIssueService
                .GetAllIssuesAsync();

        return Ok(
            new CommonResponse<
                List<BookIssueResponseDto>>
            {
                Success = true,
                Message = "Issues retrieved successfully",
                Data = result
            });
    }

    /// <summary>
    /// Retrieves a specific issue record by unique identifier.
    /// </summary>
    /// <remarks>
    /// Returns details of a single issue record identified by uniqueId.
    /// </remarks>
    /// <param name="uniqueId">The unique identifier of the issue to retrieve.</param>
    /// <returns>A CommonResponse wrapping the BookIssueResponseDto.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("{uniqueId}")]
    public async Task<IActionResult> GetIssueByUniqueIdAsync(
        string uniqueId)
    {
        var result =
            await _bookIssueService
                .GetIssueByUniqueIdAsync(
                    uniqueId);

        return Ok(
            new CommonResponse<
                BookIssueResponseDto>
            {
                Success = true,
                Message = "Issue retrieved successfully",
                Data = result
            });
    }

    /// <summary>
    /// Retrieves borrowing history for a member.
    /// </summary>
    /// <remarks>
    /// Returns all issue records for the specified member, providing their borrowing history.
    /// </remarks>
    /// <param name="memberUniqueId">The unique identifier of the member whose history to retrieve.</param>
    /// <returns>A CommonResponse wrapping a list of BookIssueResponseDto objects.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("member/{memberUniqueId}")]
    public async Task<IActionResult> GetMemberHistoryAsync(
        string memberUniqueId)
    {
        var result =
            await _bookIssueService
                .GetIssuesByMemberAsync(
                    memberUniqueId);

        return Ok(
            new CommonResponse<
                List<BookIssueResponseDto>>
            {
                Success = true,
                Message = "Member history retrieved successfully",
                Data = result
            });
    }

    /// <summary>
    /// Retrieves issue history for a book.
    /// </summary>
    /// <remarks>
    /// Returns all issue records for the specified book, providing its circulation history.
    /// </remarks>
    /// <param name="bookUniqueId">The unique identifier of the book whose history to retrieve.</param>
    /// <returns>A CommonResponse wrapping a list of BookIssueResponseDto objects.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("book/{bookUniqueId}")]
    public async Task<IActionResult> GetBookHistoryAsync(
        string bookUniqueId)
    {
        var result =
            await _bookIssueService
                .GetIssuesByBookAsync(
                    bookUniqueId);

        return Ok(
            new CommonResponse<
                List<BookIssueResponseDto>>
            {
                Success = true,
                Message = "Book history retrieved successfully",
                Data = result
            });
    }
}