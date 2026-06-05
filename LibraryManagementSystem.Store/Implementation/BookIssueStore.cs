using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Store.Abstractions;
using LibraryManagementSystem.Store.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Store.Implementations;

public class BookIssueStore : IBookIssueStore
{
    private readonly LibraryDbContext _context;

    public BookIssueStore(
        LibraryDbContext context)
    {
        _context = context;
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

    public async Task<Book?> GetBookByUniqueIdAsync(
        string uniqueId)
    {
        try
        {
            return await _context.Books
                .FirstOrDefaultAsync(x =>
                    x.UniqueId == uniqueId &&
                    x.IsActive);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetBookByUniqueIdAsync)}: {ex.Message}");
        }
    }

    public async Task<Book?> GetBookByIdAsync(
        int bookId)
    {
        try
        {
            return await _context.Books
                .FirstOrDefaultAsync(x =>
                    x.BookId == bookId);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetBookByIdAsync)}: {ex.Message}");
        }
    }

    public async Task<BookIssue> CreateIssueAsync(
        BookIssue issue)
    {
        try
        {
            await _context.BookIssues
                .AddAsync(issue);

            await _context.SaveChangesAsync();

            return issue;
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(CreateIssueAsync)}: {ex.Message}");
        }
    }

    public async Task<BookIssue?> GetIssueByUniqueIdAsync(
        string uniqueId)
    {
        try
        {
            return await _context.BookIssues
                .FirstOrDefaultAsync(x =>
                    x.UniqueId == uniqueId &&
                    x.IsActive);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetIssueByUniqueIdAsync)}: {ex.Message}");
        }
    }

    public async Task UpdateIssueAsync(
        BookIssue issue)
    {
        try
        {
            _context.BookIssues.Update(issue);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(UpdateIssueAsync)}: {ex.Message}");
        }
    }

    public async Task UpdateBookAsync(
        Book book)
    {
        try
        {
            _context.Books.Update(book);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(UpdateBookAsync)}: {ex.Message}");
        }
    }

    public async Task CreateFineAsync(
        Fine fine)
    {
        try
        {
            await _context.Fines.AddAsync(fine);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(CreateFineAsync)}: {ex.Message}");
        }
    }

    public async Task<List<BookIssue>> GetAllIssuesAsync()
    {
        try
        {
            return await (
                from issue in _context.BookIssues
                join member in _context.Members
                    on issue.MemberId equals member.MemberId
                join book in _context.Books
                    on issue.BookId equals book.BookId
                where issue.IsActive
                select new BookIssue
                {
                    IssueId = issue.IssueId,

                    UniqueId = issue.UniqueId,

                    IssueDate = issue.IssueDate,

                    DueDate = issue.DueDate,

                    ReturnDate = issue.ReturnDate,

                    Status = issue.Status,

                    MemberName = member.FullName,

                    BookTitle = book.Title
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetAllIssuesAsync)}: {ex.Message}");
        }
    }

    public async Task<BookIssue?> GetIssueDetailsByUniqueIdAsync(
    string uniqueId)
    {
        try
        {
            return await (
                from issue in _context.BookIssues
                join member in _context.Members
                    on issue.MemberId equals member.MemberId
                join book in _context.Books
                    on issue.BookId equals book.BookId
                where issue.UniqueId == uniqueId
                      && issue.IsActive
                select new BookIssue
                {
                    IssueId = issue.IssueId,

                    UniqueId = issue.UniqueId,

                    IssueDate = issue.IssueDate,

                    DueDate = issue.DueDate,

                    ReturnDate = issue.ReturnDate,

                    Status = issue.Status,

                    MemberName = member.FullName,

                    BookTitle = book.Title
                })
                .FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetIssueDetailsByUniqueIdAsync)}: {ex.Message}");
        }
    }

    public async Task<List<BookIssue>>
    GetIssuesByMemberUniqueIdAsync(
        string memberUniqueId)
    {
        try
        {
            return await (
                from issue in _context.BookIssues
                join member in _context.Members
                    on issue.MemberId equals member.MemberId
                join book in _context.Books
                    on issue.BookId equals book.BookId
                where member.UniqueId == memberUniqueId
                      && issue.IsActive
                select new BookIssue
                {
                    IssueId = issue.IssueId,

                    UniqueId = issue.UniqueId,

                    IssueDate = issue.IssueDate,

                    DueDate = issue.DueDate,

                    ReturnDate = issue.ReturnDate,

                    Status = issue.Status,

                    MemberName = member.FullName,

                    BookTitle = book.Title
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetIssuesByMemberUniqueIdAsync)}: {ex.Message}");
        }
    }

    public async Task<List<BookIssue>>
    GetIssuesByBookUniqueIdAsync(
        string bookUniqueId)
    {
        try
        {
            return await (
                from issue in _context.BookIssues
                join member in _context.Members
                    on issue.MemberId equals member.MemberId
                join book in _context.Books
                    on issue.BookId equals book.BookId
                where book.UniqueId == bookUniqueId
                      && issue.IsActive
                select new BookIssue
                {
                    IssueId = issue.IssueId,

                    UniqueId = issue.UniqueId,

                    IssueDate = issue.IssueDate,

                    DueDate = issue.DueDate,

                    ReturnDate = issue.ReturnDate,

                    Status = issue.Status,

                    MemberName = member.FullName,

                    BookTitle = book.Title
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetIssuesByBookUniqueIdAsync)}: {ex.Message}");
        }
    }
}