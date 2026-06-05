using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Store.Abstractions;
using LibraryManagementSystem.Store.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Store.Implementations;

public class AuthStore : IAuthStore
{
    private readonly LibraryDbContext _context;

    public AuthStore(
        LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(
        string email)
    {
        try
        {
            var result = await (
                from u in _context.Users
                join r in _context.Roles
                    on u.RoleId equals r.RoleId
                where u.Email == email
                      && u.IsActive
                select new
                {
                    User = u,
                    RoleName = r.RoleName
                })
                .FirstOrDefaultAsync();

            if (result == null)
            {
                return null;
            }

            result.User.RoleName = result.RoleName;

            return result.User;
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetUserByEmailAsync)}: {ex.Message}");
        }
    }
}