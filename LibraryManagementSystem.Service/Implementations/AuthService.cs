using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;
using LibraryManagementSystem.Common.Helpers;
using LibraryManagementSystem.Service.Abstractions;
using LibraryManagementSystem.Store.Abstractions;
using Serilog;

namespace LibraryManagementSystem.Service.Implementations;

public class AuthService : IAuthService
{
    private readonly IAuthStore _authStore;
    private readonly JwtHelper _jwtHelper;

    public AuthService(
        IAuthStore authStore,
        JwtHelper jwtHelper)
    {
        _authStore = authStore;
        _jwtHelper = jwtHelper;
    }

    public async Task<LoginResponseDto> LoginAsync(
        LoginRequestDto request)
    {
        try
        {
            var user = await _authStore
                .GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                Log.Warning(
            "Failed login attempt for {Email}",
            request.Email);
                throw new Exception(
                    "Invalid Email or Password");
            }

            var isPasswordValid =
                PasswordHelper.VerifyPassword(
                    request.Password,
                    user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new Exception(
                    "Invalid Email or Password");
            }

            var token =
                _jwtHelper.GenerateToken(
                    user,
                    user.RoleName);

            Log.Information(
                "User {Email} logged in successfully",
                user.Email);

            return new LoginResponseDto
            {
                UniqueId = user.UniqueId,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.RoleName,
                Token = token
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(LoginAsync)}: {ex.Message}");
        }
    }
}