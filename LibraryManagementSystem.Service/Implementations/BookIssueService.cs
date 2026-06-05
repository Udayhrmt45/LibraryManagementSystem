using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;
using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Service.Abstractions;
using LibraryManagementSystem.Store.Abstractions;

namespace LibraryManagementSystem.Service.Implementations;

public class BookIssueService : IBookIssueService
{
    private readonly IBookIssueStore _bookIssueStore;

    public BookIssueService(
        IBookIssueStore bookIssueStore)
    {
        _bookIssueStore = bookIssueStore;
    }

    public async Task<BookIssueResponseDto> IssueBookAsync(
        IssueBookRequestDto request)
    {
        try
        {
            var member =
                await _bookIssueStore
                    .GetMemberByUniqueIdAsync(
                        request.MemberUniqueId);

            if (member == null)
            {
                throw new Exception(
                    "Member not found");
            }

            var book =
                await _bookIssueStore
                    .GetBookByUniqueIdAsync(
                        request.BookUniqueId);

            if (book == null)
            {
                throw new Exception(
                    "Book not found");
            }

            if (book.AvailableCopies <= 0)
            {
                throw new Exception(
                    "Book is not available");
            }

            var issue = new BookIssue
            {
                MemberId = member.MemberId,

                BookId = book.BookId,

                IssueDate = DateTime.UtcNow,

                DueDate =
                    DateTime.UtcNow.AddDays(
                        request.BorrowDays),

                Status = "Issued",

                IsActive = true,

                CreatedOn = DateTime.UtcNow
            };

            book.AvailableCopies--;

            await _bookIssueStore
                .UpdateBookAsync(book);

            var createdIssue =
                await _bookIssueStore
                    .CreateIssueAsync(issue);

            return new BookIssueResponseDto
            {
                IssueUniqueId =
                    createdIssue.UniqueId,

                MemberName =
                    member.FullName,

                BookTitle =
                    book.Title,

                IssueDate =
                    createdIssue.IssueDate,

                DueDate =
                    createdIssue.DueDate,

                ReturnDate =
                    createdIssue.ReturnDate,

                Status =
                    createdIssue.Status
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(IssueBookAsync)}: {ex.Message}");
        }
    }

    public async Task ReturnBookAsync(
        ReturnBookRequestDto request)
    {
        try
        {
            var issue =
                await _bookIssueStore
                    .GetIssueByUniqueIdAsync(
                        request.IssueUniqueId);

            if (issue == null)
            {
                throw new Exception(
                    "Issue record not found");
            }

            if (issue.Status == "Returned")
            {
                throw new Exception(
                    "Book already returned");
            }

            issue.ReturnDate =
                DateTime.UtcNow;

            issue.Status =
                "Returned";

            issue.UpdatedOn =
                DateTime.UtcNow;

            await _bookIssueStore
                .UpdateIssueAsync(issue);

            var book =
                await _bookIssueStore
                    .GetBookByIdAsync(
                        issue.BookId);

            if (book != null)
            {
                book.AvailableCopies++;

                book.UpdatedOn =
                    DateTime.UtcNow;

                await _bookIssueStore
                    .UpdateBookAsync(book);
            }

            if (issue.ReturnDate >
                issue.DueDate)
            {
                var lateDays =
                    (issue.ReturnDate.Value -
                     issue.DueDate).Days;

                var fine = new Fine
                {
                    IssueId =
                        issue.IssueId,

                    FineAmount =
                        lateDays * 10,

                    IsPaid = false,

                    IsActive = true,

                    CreatedOn =
                        DateTime.UtcNow
                };

                await _bookIssueStore
                    .CreateFineAsync(fine);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(ReturnBookAsync)}: {ex.Message}");
        }
    }

    private static BookIssueResponseDto MapIssue(
    BookIssue issue)
    {
        return new BookIssueResponseDto
        {
            IssueUniqueId = issue.UniqueId,

            MemberName = issue.MemberName,

            BookTitle = issue.BookTitle,

            IssueDate = issue.IssueDate,

            DueDate = issue.DueDate,

            ReturnDate = issue.ReturnDate,

            Status = issue.Status
        };
    }

    public async Task<List<BookIssueResponseDto>>
    GetAllIssuesAsync()
    {
        try
        {
            var issues =
                await _bookIssueStore
                    .GetAllIssuesAsync();

            return issues
                .Select(MapIssue)
                .ToList();
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetAllIssuesAsync)}: {ex.Message}");
        }
    }

    public async Task<BookIssueResponseDto>
    GetIssueByUniqueIdAsync(
        string uniqueId)
    {
        try
        {
            var issue =
                await _bookIssueStore
                    .GetIssueDetailsByUniqueIdAsync(
                        uniqueId);

            if (issue == null)
            {
                throw new Exception(
                    "Issue not found");
            }

            return MapIssue(issue);
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetIssueByUniqueIdAsync)}: {ex.Message}");
        }
    }

    public async Task<List<BookIssueResponseDto>>
    GetIssuesByMemberAsync(
        string memberUniqueId)
    {
        try
        {
            var issues =
                await _bookIssueStore
                    .GetIssuesByMemberUniqueIdAsync(
                        memberUniqueId);

            return issues
                .Select(MapIssue)
                .ToList();
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetIssuesByMemberAsync)}: {ex.Message}");
        }
    }

    public async Task<List<BookIssueResponseDto>>
    GetIssuesByBookAsync(
        string bookUniqueId)
    {
        try
        {
            var issues =
                await _bookIssueStore
                    .GetIssuesByBookUniqueIdAsync(
                        bookUniqueId);

            return issues
                .Select(MapIssue)
                .ToList();
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetIssuesByBookAsync)}: {ex.Message}");
        }
    }
}