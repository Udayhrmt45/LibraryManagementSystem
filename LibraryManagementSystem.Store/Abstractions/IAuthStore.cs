using LibraryManagementSystem.Common.Models;

namespace LibraryManagementSystem.Store.Abstractions;

public interface IAuthStore
{
    Task<User?> GetUserByEmailAsync(
        string email);
}