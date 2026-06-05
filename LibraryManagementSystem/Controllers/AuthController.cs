using LibraryManagementSystem.Common;
using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;
using LibraryManagementSystem.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
/// <summary>
/// Authentication controller responsible for user sign-in and token issuance.
/// </summary>
/// <remarks>
/// Exposes login endpoint which validates user credentials and returns a JWT token
/// and related authentication information. Anonymous access is allowed for login.
/// </remarks>
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(
        IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Authenticates a user and issues a JWT token.
    /// </summary>
    /// <remarks>
    /// Validates the provided credentials and, on success, returns a JWT access token
    /// and related authentication details in the response. The token should be used
    /// for subsequent authorized requests and is validated by the application's JWT
    /// authentication middleware.
    /// </remarks>
    /// <param name="request">The login request containing username/email and password.</param>
    /// <returns>A CommonResponse wrapping the LoginResponseDto containing token and user info.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginRequestDto request)
    {
        var response =
            await _authService.LoginAsync(request);

        return Ok(
            new CommonResponse<LoginResponseDto>
            {
                Success = true,
                Message = "Login Successful",
                Data = response
            });
    }
    
}