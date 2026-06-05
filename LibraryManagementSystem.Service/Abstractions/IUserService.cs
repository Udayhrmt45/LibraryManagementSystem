using LibraryManagementSystem.Common.DTOs.Request;
using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;

namespace LibraryManagementSystem.Service.Abstractions;

public interface IUserService
{
    Task<UserResponseDto> CreateUserAsync(
        CreateUserRequestDto request);

    Task<List<UserResponseDto>> GetAllUsersAsync();

    Task<UserResponseDto> GetUserByUniqueIdAsync(
        string uniqueId);

    Task UpdateUserAsync(
        string uniqueId,
        UpdateUserRequestDto request);

    Task DeleteUserAsync(
        string uniqueId);
}