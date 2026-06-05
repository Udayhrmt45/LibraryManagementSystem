using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Store.Abstractions;
using LibraryManagementSystem.Store.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Store.Implementations;

public class UserStore : IUserStore
{
    private readonly LibraryDbContext _context;

    public UserStore(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        try
        {
            return await _context.Users
                .AnyAsync(x =>
                    x.Email == email &&
                    x.IsActive);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(EmailExistsAsync)}: {ex.Message}");
        }
    }

    public async Task<User> CreateUserAsync(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return user;
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(CreateUserAsync)}: {ex.Message}");
        }
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        try
        {
            return await (
                from u in _context.Users
                join r in _context.Roles
                    on u.RoleId equals r.RoleId
                where u.IsActive
                select new User
                {
                    UserId = u.UserId,
                    UniqueId = u.UniqueId,
                    FullName = u.FullName,
                    Email = u.Email,
                    RoleId = u.RoleId,
                    RoleName = r.RoleName
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetAllUsersAsync)}: {ex.Message}");
        }
    }

    public async Task<Role?> GetRoleByUniqueIdAsync(
    string roleUniqueId)
    {
        try
        {
            return await _context.Roles
                .FirstOrDefaultAsync(x =>
                    x.UniqueId == roleUniqueId &&
                    x.IsActive);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetRoleByUniqueIdAsync)}: {ex.Message}");
        }
    }

    public async Task<User?> GetUserByUniqueIdAsync(string uniqueId)
    {
        try
        {
            return await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.UniqueId == uniqueId &&
                    x.IsActive);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetUserByUniqueIdAsync)}: {ex.Message}");
        }
    }

    public async Task UpdateUserAsync(User user)
    {
        try
        {
            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(UpdateUserAsync)}: {ex.Message}");
        }
    }

    public async Task SoftDeleteUserAsync(User user)
    {
        try
        {
            user.IsActive = false;

            user.UpdatedOn = DateTime.UtcNow;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(SoftDeleteUserAsync)}: {ex.Message}");
        }
    }
}