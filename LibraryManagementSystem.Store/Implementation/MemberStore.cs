using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Store.Abstractions;
using LibraryManagementSystem.Store.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Store.Implementations;

public class MemberStore : IMemberStore
{
    private readonly LibraryDbContext _context;

    public MemberStore(
        LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<Member> CreateMemberAsync(
        Member Member)
    {
        try
        {
            await _context.Members.AddAsync(Member);

            await _context.SaveChangesAsync();

            return Member;
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(CreateMemberAsync)}: {ex.Message}");
        }
    }

    public async Task<List<Member>> GetAllMembersAsync()
    {
        try
        {
            return await _context.Members
                .Where(x => x.IsActive)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetAllMembersAsync)}: {ex.Message}");
        }
    }

    public async Task<Member?> GetMemberByUniqueIdAsync(
        string uniqueId)
    {
        try
        {
            return await _context.Members
                .FirstOrDefaultAsync(x =>
                    x.UniqueId == uniqueId &&
                    x.IsActive);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetMemberByUniqueIdAsync)}: {ex.Message}");
        }
    }

    public async Task<(List<Member> Members, int TotalRecords)>
    GetMembersPagedAsync(
        int pageNumber,
        int pageSize)
    {
        try
        {
            var query =
                _context.Members
                    .Where(x => x.IsActive);

            var totalRecords =
                await query.CountAsync();

            var Members =
                await query
                    .OrderBy(x => x.MemberId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            return (Members, totalRecords);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetMembersPagedAsync)}: {ex.Message}");
        }
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        try
        {
            return await _context.Members
                .AnyAsync(x =>
                    x.Email == email &&
                    x.IsActive);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(EmailExistsAsync)}: {ex.Message}");
        }
    }

    public async Task UpdateMemberAsync(Member Member)
    {
        try
        {
            _context.Members.Update(Member);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(UpdateMemberAsync)}: {ex.Message}");
        }
    }

    public async Task SoftDeleteMemberAsync(Member Member)
    {
        try
        {
            Member.IsActive = false;

            Member.UpdatedOn = DateTime.UtcNow;

            _context.Members.Update(Member);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(SoftDeleteMemberAsync)}: {ex.Message}");
        }
    }
}