using LibraryManagementSystem.Common;
using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;
using LibraryManagementSystem.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using LibraryManagementSystem.Common.Constants;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers;

/// <summary>
/// Controller for managing application users. Provides endpoints to create, retrieve, update and delete users.
/// </summary>
/// <remarks>
/// All endpoints on this controller require the caller to be authorized with the Admin role.
/// The controller coordinates user CRUD operations via the IUserService abstraction.
/// </remarks>
[Authorize(Roles = AppConstants.AdminRole)]
[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Creates a new user in the system.
    /// </summary>
    /// <remarks>
    /// Accepts a CreateUserRequestDto containing the user's details and delegates
    /// creation to the user service. On success, returns the newly created user's data.
    /// </remarks>
    /// <param name="request">
    /// The user creation request containing name, email, role and other required properties.
    /// </param>
    /// <returns>
    /// A CommonResponse wrapping the created UserResponseDto.
    /// </returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync(
        CreateUserRequestDto request)
    {
        var result =
            await _userService.CreateUserAsync(
                request);

        return Ok(
            new CommonResponse<UserResponseDto>
            {
                Success = true,
                Message = "User created successfully",
                Data = result
            });
    }

    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <remarks>
    /// Returns a list of all users available in the system. This endpoint is useful for
    /// administrative scenarios where an overview of all user accounts is required.
    /// </remarks>
    /// <returns>
    /// A CommonResponse wrapping a list of UserResponseDto objects.
    /// </returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var result =
            await _userService.GetAllUsersAsync();

        return Ok(
            new CommonResponse<List<UserResponseDto>>
            {
                Success = true,
                Message = "Users retrieved successfully",
                Data = result
            });
    }

    /// <summary>
    /// Retrieves a single user by its unique identifier.
    /// </summary>
    /// <remarks>
    /// Looks up a user by the provided uniqueId and returns detailed user information
    /// if a matching user exists.
    /// </remarks>
    /// <param name="uniqueId">
    /// The unique identifier of the user to retrieve.
    /// </param>
    /// <returns>
    /// A CommonResponse wrapping the UserResponseDto for the requested user.
    /// </returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("{uniqueId}")]
    public async Task<IActionResult> GetUserByUniqueIdAsync(
        string uniqueId)
    {
        var result =
            await _userService
                .GetUserByUniqueIdAsync(
                    uniqueId);

        return Ok(
            new CommonResponse<UserResponseDto>
            {
                Success = true,
                Message = "User retrieved successfully",
                Data = result
            });
    }

    /// <summary>
    /// Updates an existing user's information.
    /// </summary>
    /// <remarks>
    /// Updates user details for the user identified by uniqueId using the values provided
    /// in the UpdateUserRequestDto. Only authorized Admin role users may perform this action.
    /// </remarks>
    /// <param name="uniqueId">
    /// The unique identifier of the user to update.
    /// </param>
    /// <param name="request">
    /// The update request containing fields to be changed for the user.
    /// </param>
    /// <returns>
    /// A CommonResponse indicating success or failure of the update operation.
    /// </returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPut("{uniqueId}")]
    public async Task<IActionResult> UpdateUserAsync(
        string uniqueId,
        UpdateUserRequestDto request)
    {
        await _userService
            .UpdateUserAsync(
                uniqueId,
                request);

        return Ok(
            new CommonResponse<string>
            {
                Success = true,
                Message = "User updated successfully",
                Data = null
            });
    }

    /// <summary>
    /// Deletes a user from the system.
    /// </summary>
    /// <remarks>
    /// Deletes the user identified by uniqueId. This operation is intended for administrative
    /// use and requires the Admin role. The deletion is performed by the underlying service.
    /// </remarks>
    /// <param name="uniqueId">
    /// The unique identifier of the user to delete.
    /// </param>
    /// <returns>
    /// A CommonResponse indicating success or failure of the delete operation.
    /// </returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpDelete("{uniqueId}")]
    public async Task<IActionResult> DeleteUserAsync(
        string uniqueId)
    {
        await _userService
            .DeleteUserAsync(
                uniqueId);

        return Ok(
            new CommonResponse<string>
            {
                Success = true,
                Message = "User deleted successfully",
                Data = null
            });
    }
}