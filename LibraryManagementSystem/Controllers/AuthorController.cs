using LibraryManagementSystem.Common;
using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;
using LibraryManagementSystem.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
/// <summary>
/// Controller for managing authors in the library system.
/// </summary>
/// <remarks>
/// Provides CRUD operations for author entities. Requires an authenticated user.
/// </remarks>
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorController(
        IAuthorService authorService)
    {
        _authorService = authorService;
    }

    /// <summary>
    /// Creates a new author.
    /// </summary>
    /// <remarks>
    /// Creates an author record in the database.
    /// </remarks>
    /// <param name="request">The author creation request containing name and biography.</param>
    /// <returns>A CommonResponse wrapping the created AuthorResponseDto.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost]
    public async Task<IActionResult> CreateAuthorAsync(CreateAuthorRequestDto request)
    {
        var result =
            await _authorService
                .CreateAuthorAsync(request);

        return Ok(
            new CommonResponse<AuthorResponseDto>
            {
                Success = true,
                Message = "Author created successfully",
                Data = result
            });
    }

    /// <summary>
    /// Retrieves all authors.
    /// </summary>
    /// <remarks>
    /// Returns a list of all authors available in the system.
    /// </remarks>
    /// <returns>A CommonResponse wrapping a list of AuthorResponseDto objects.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    public async Task<IActionResult> GetAllAuthorsAsync()
    {
        var result =
            await _authorService
                .GetAllAuthorsAsync();

        return Ok(
            new CommonResponse<List<AuthorResponseDto>>
            {
                Success = true,
                Message = "Authors retrieved successfully",
                Data = result
            });
    }

    /// <summary>
    /// Retrieves an author by unique identifier.
    /// </summary>
    /// <remarks>
    /// Returns author details for the specified uniqueId.
    /// </remarks>
    /// <param name="uniqueId">The unique identifier of the author to retrieve.</param>
    /// <returns>A CommonResponse wrapping the AuthorResponseDto.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("{uniqueId}")]
    public async Task<IActionResult> GetAuthorByUniqueIdAsync(string uniqueId)
    {
        var result =
            await _authorService
                .GetAuthorByUniqueIdAsync(
                    uniqueId);

        return Ok(
            new CommonResponse<AuthorResponseDto>
            {
                Success = true,
                Message = "Author retrieved successfully",
                Data = result
            });
    }

    /// <summary>
    /// Retrieves authors with pagination.
    /// </summary>
    /// <remarks>
    /// Returns a paged list of authors including pagination metadata such as page number,
    /// page size and total records.
    /// </remarks>
    /// <param name="request">Pagination request containing page number and page size.</param>
    /// <returns>A CommonResponse wrapping a PagedResponseDto of AuthorResponseDto.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    public async Task<IActionResult> GetAuthorsPagedAsync([FromQuery] PaginationRequestDto request)
    {
        var result =
            await _authorService
                .GetAuthorsPagedAsync(request);

        return Ok(
            new CommonResponse<
                PagedResponseDto<AuthorResponseDto>>
            {
                Success = true,
                Message = "Authors retrieved successfully",
                Data = result
            });
    }

    /// <summary>
    /// Updates an existing author.
    /// </summary>
    /// <remarks>
    /// Updates author data identified by uniqueId with the supplied values.
    /// </remarks>
    /// <param name="uniqueId">The unique identifier of the author to update.</param>
    /// <param name="request">The update request containing updated author fields.</param>
    /// <returns>A CommonResponse indicating the update result.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPut("{uniqueId}")]
    public async Task<IActionResult> UpdateAuthorAsync(string uniqueId, UpdateAuthorRequestDto request)
    {
        await _authorService
            .UpdateAuthorAsync(
                uniqueId,
                request);

        return Ok(
            new CommonResponse<string>
            {
                Success = true,
                Message = "Author updated successfully"
            });
    }

    /// <summary>
    /// Deletes an author.
    /// </summary>
    /// <remarks>
    /// Deletes the author identified by uniqueId.
    /// </remarks>
    /// <param name="uniqueId">The unique identifier of the author to delete.</param>
    /// <returns>A CommonResponse indicating the deletion result.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpDelete("{uniqueId}")]
    public async Task<IActionResult> DeleteAuthorAsync(string uniqueId)
    {
        await _authorService
            .DeleteAuthorAsync(
                uniqueId);

        return Ok(
            new CommonResponse<string>
            {
                Success = true,
                Message = "Author deleted successfully"
            });
    }
}