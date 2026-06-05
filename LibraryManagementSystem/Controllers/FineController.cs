using LibraryManagementSystem.Common;
using LibraryManagementSystem.Common.DTOs.Responses;
using LibraryManagementSystem.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers;

[Authorize(Roles = "Admin,Librarian")]
[ApiController]
[Route("api/[controller]/[action]")]
/// <summary>
/// Controller for managing fines related to book returns.
/// </summary>
/// <remarks>
/// Provides endpoints to view fines, calculate fine amounts and process fine payments. Requires Admin or Librarian role.
/// </remarks>
public class FineController : ControllerBase
{
    private readonly IFineService _fineService;

    public FineController(
        IFineService fineService)
    {
        _fineService = fineService;
    }

    /// <summary>
    /// Retrieves all fines.
    /// </summary>
    /// <remarks>
    /// Returns a list of all fines recorded in the system.
    /// </remarks>
    /// <returns>A CommonResponse wrapping a list of FineResponseDto objects.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    public async Task<IActionResult> GetAllFinesAsync()
    {
        var result =
            await _fineService
                .GetAllFinesAsync();

        return Ok(
            new CommonResponse<
                List<FineResponseDto>>
            {
                Success = true,
                Message = "Fines retrieved successfully",
                Data = result
            });
    }

    /// <summary>
    /// Retrieves a fine by unique identifier.
    /// </summary>
    /// <remarks>
    /// Returns fine details for the provided uniqueId.
    /// </remarks>
    /// <param name="uniqueId">The unique identifier of the fine to retrieve.</param>
    /// <returns>A CommonResponse wrapping the FineResponseDto.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("{uniqueId}")]
    public async Task<IActionResult> GetFineByUniqueIdAsync(
            string uniqueId)
    {
        var result =
            await _fineService
                .GetFineByUniqueIdAsync(
                    uniqueId);

        return Ok(
            new CommonResponse<
                FineResponseDto>
            {
                Success = true,
                Message = "Fine retrieved successfully",
                Data = result
            });
    }

    /// <summary>
    /// Retrieves fine details.
    /// </summary>
    /// <remarks>
    /// Returns aggregated fine details and any metadata required by the client.
    /// </remarks>
    /// <returns>Fine details or service response.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    public async Task<IActionResult> GetFineDetailsAsync()
    {
        return Ok(
            await _fineService
                .GetFineDetailsAsync());
    }


    /// <summary>
    /// Calculates fine amount based on due and return dates.
    /// </summary>
    /// <remarks>
    /// Computes the fine for a late return using business rules implemented in the fine service.
    /// </remarks>
    /// <param name="dueDate">The original due date for the borrowed item.</param>
    /// <param name="returnDate">The actual return date.</param>
    /// <returns>An anonymous object containing due date, return date and calculated fine amount.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    public async Task<IActionResult> CalculateFineAsync(
        DateTime dueDate,
        DateTime returnDate)
    {
        var fine =
            await _fineService
                .CalculateFineAsync(
                    dueDate,
                    returnDate);

        return Ok(
            new
            {
                DueDate = dueDate,
                ReturnDate = returnDate,
                FineAmount = fine
            });
    }

    /// <summary>
    /// Processes fine payment for a fine record.
    /// </summary>
    /// <remarks>
    /// Marks the fine identified by uniqueId as paid using the fine service.
    /// </remarks>
    /// <param name="uniqueId">The unique identifier of the fine to pay.</param>
    /// <returns>A CommonResponse indicating successful payment.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPut("pay/{uniqueId}")]
    public async Task<IActionResult> PayFineAsync(string uniqueId)
    {
        await _fineService
            .PayFineAsync(
                uniqueId);

        return Ok(
            new CommonResponse<string>
            {
                Success = true,
                Message = "Fine paid successfully"
            });
    }
}