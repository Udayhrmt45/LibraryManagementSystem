using LibraryManagementSystem.Common.Models;

namespace LibraryManagementSystem.Store.Abstractions;

public interface IUserStore
{
    Task<bool> EmailExistsAsync(string email);

    Task<User> CreateUserAsync(User user);

    Task<List<User>> GetAllUsersAsync();

    Task<User?> GetUserByUniqueIdAsync(string uniqueId);

    Task<Role?> GetRoleByUniqueIdAsync(
        string roleUniqueId);

    Task UpdateUserAsync(User user);

    Task SoftDeleteUserAsync(User user);
}