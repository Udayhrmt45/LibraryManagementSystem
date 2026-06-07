using LibraryManagementSystem.Common;
using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
/// <summary>
/// Controller for managing book categories.
/// </summary>
/// <remarks>
/// Provides endpoints to create, retrieve, update and delete categories. Requires authentication.
/// </remarks>
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(
        ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <remarks>
    /// Creates a category using the supplied CreateCategoryRequestDto.
    /// </remarks>
    /// <param name="request">Category creation request containing name and description.</param>
    /// <returns>The created category or service response.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost]
    public async Task<IActionResult> CreateCategoryAsync(
            CreateCategoryRequestDto request)
    {
        var result =
            await _categoryService
                .CreateCategoryAsync(request);

        return Ok(result);
    }

    /// <summary>
    /// Retrieves all categories.
    /// </summary>
    /// <remarks>
    /// Returns a list of all book categories available in the system.
    /// </remarks>
    /// <returns>A collection of categories.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    public async Task<IActionResult> GetAllCategoriesAsync()
    {
        return Ok(
            await _categoryService
                .GetAllCategoriesAsync());
    }

    /// <summary>
    /// Retrieves categories with pagination.
    /// </summary>
    /// <remarks>
    /// Returns a paged list of categories with pagination metadata (page number, page size, total records).
    /// </remarks>
    /// <param name="request">Pagination request containing page number and page size.</param>
    /// <returns>Paged categories response.
    /// </returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    public async Task<IActionResult> GetCategoriesPagedAsync(
            [FromQuery]
            PaginationRequestDto request)
    {
        return Ok(
            await _categoryService
                .GetCategoriesPagedAsync(request));
    }

    /// <summary>
    /// Retrieves a category by its unique identifier.
    /// </summary>
    /// <remarks>
    /// Returns category details for the provided uniqueId.
    /// </remarks>
    /// <param name="uniqueId">The unique identifier of the category to retrieve.</param>
    /// <returns>The category details.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("{uniqueId}")]
    public async Task<IActionResult> GetCategoryByUniqueIdAsync(
            string uniqueId)
    {
        return Ok(
            await _categoryService
                .GetCategoryByUniqueIdAsync(
                    uniqueId));
    }

    /// <summary>
    /// Bulk inserts multiple categories.
    /// </summary>
    /// <response code="200">
    /// Categories inserted successfully.
    /// </response>
    [HttpPost]
    public async Task<IActionResult>
        BulkInsertCategoriesAsync(
            [FromBody]
        BulkInsertCategoriesRequestDto request)
    {
        await _categoryService
            .BulkInsertCategoriesAsync(request);

        return Ok(
            new CommonResponse<string>
            {
                Success = true,
                Message =
                    "Categories inserted successfully."
            });
    }

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <remarks>
    /// Updates category data for the category identified by uniqueId.
    /// </remarks>
    /// <param name="uniqueId">The unique identifier of the category to update.</param>
    /// <param name="request">The update request containing category fields to modify.</param>
    /// <returns>Confirmation message on successful update.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPut("{uniqueId}")]
    public async Task<IActionResult> UpdateCategoryAsync(string uniqueId,
            UpdateCategoryRequestDto request)
    {
        await _categoryService
            .UpdateCategoryAsync(
                uniqueId,
                request);

        return Ok("Category updated successfully");
    }

    /// <summary>
    /// Deletes a category.
    /// </summary>
    /// <remarks>
    /// Deletes the category identified by uniqueId from the system.
    /// </remarks>
    /// <param name="uniqueId">The unique identifier of the category to delete.</param>
    /// <returns>Confirmation message on successful deletion.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpDelete("{uniqueId}")]
    public async Task<IActionResult> DeleteCategoryAsync(string uniqueId)
    {
        await _categoryService
            .DeleteCategoryAsync(
                uniqueId);

        return Ok("Category deleted successfully");
    }
}