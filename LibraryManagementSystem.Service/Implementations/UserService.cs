using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;
using LibraryManagementSystem.Common.Helpers;
using LibraryManagementSystem.Service.Abstractions;
using LibraryManagementSystem.Store.Abstractions;

namespace LibraryManagementSystem.Service.Implementations;

public class UserService : IUserService
{
    private readonly IUserStore _userStore;

    public UserService(IUserStore userStore)
    {
        _userStore = userStore;
    }

    public async Task<UserResponseDto> CreateUserAsync(
        CreateUserRequestDto request)
    {
        try
        {
            var emailExists =
                await _userStore.EmailExistsAsync(
                    request.Email);

            if (emailExists)
            {
                throw new Exception(
                    "Email already exists");
            }

            var role =
                await _userStore.GetRoleByUniqueIdAsync(
                    request.RoleUniqueId);

            if (role == null)
            {
                throw new Exception(
                    "Invalid Role");
            }

            var user = new User
            {
                RoleId = role.RoleId,

                FullName = request.FullName,

                Email = request.Email,

                PasswordHash =
            PasswordHelper.HashPassword(
                request.Password),

                IsActive = true,

                CreatedOn = DateTime.UtcNow
            };

            var createdUser =
                await _userStore.CreateUserAsync(user);

            return new UserResponseDto
            {
                UniqueId = createdUser.UniqueId,

                FullName = createdUser.FullName,

                Email = createdUser.Email,

                RoleName = role.RoleName
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(CreateUserAsync)}: {ex.Message}");
        }
    }

    public async Task<List<UserResponseDto>>
        GetAllUsersAsync()
    {
        try
        {
            var users =
                await _userStore.GetAllUsersAsync();

            return users.Select(user =>
                new UserResponseDto
                {
                    UniqueId = user.UniqueId,

                    FullName = user.FullName,

                    Email = user.Email,

                    RoleName = user.RoleName
                }).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetAllUsersAsync)}: {ex.Message}");
        }
    }

    public async Task<UserResponseDto>
        GetUserByUniqueIdAsync(
            string uniqueId)
    {
        try
        {
            var user =
                await _userStore
                    .GetUserByUniqueIdAsync(
                        uniqueId);

            if (user == null)
            {
                throw new Exception(
                    "User not found");
            }

            return new UserResponseDto
            {
                UniqueId = user.UniqueId,

                FullName = user.FullName,

                Email = user.Email,

                RoleName = user.RoleName
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetUserByUniqueIdAsync)}: {ex.Message}");
        }
    }

    public async Task UpdateUserAsync(
        string uniqueId,
        UpdateUserRequestDto request)
    {
        try
        {
            var user =
                await _userStore
                    .GetUserByUniqueIdAsync(
                        uniqueId);

            if (user == null)
            {
                throw new Exception(
                    "User not found");
            }

            var role =
                await _userStore
                    .GetRoleByUniqueIdAsync(
                        request.RoleUniqueId);

            if (role == null)
            {
                throw new Exception(
                    "Invalid Role");
            }

            user.RoleId = role.RoleId;

            user.FullName = request.FullName;

            user.Email = request.Email;

            user.UpdatedOn = DateTime.UtcNow;

            await _userStore.UpdateUserAsync(
                user);
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(UpdateUserAsync)}: {ex.Message}");
        }
    }

    public async Task DeleteUserAsync(
        string uniqueId)
    {
        try
        {
            var user =
                await _userStore
                    .GetUserByUniqueIdAsync(
                        uniqueId);

            if (user == null)
            {
                throw new Exception(
                    "User not found");
            }

            await _userStore
                .SoftDeleteUserAsync(user);
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(DeleteUserAsync)}: {ex.Message}");
        }
    }
}