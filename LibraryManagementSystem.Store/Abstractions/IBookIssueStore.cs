using LibraryManagementSystem.Common.Models;

namespace LibraryManagementSystem.Store.Abstractions;

public interface IBookIssueStore
{
    Task<Member?> GetMemberByUniqueIdAsync(
        string uniqueId);

    Task<Book?> GetBookByUniqueIdAsync(
        string uniqueId);

    Task<Book?> GetBookByIdAsync(
        int bookId);

    Task<BookIssue> CreateIssueAsync(
        BookIssue issue);

    Task<BookIssue?> GetIssueByUniqueIdAsync(
        string uniqueId);

    Task UpdateIssueAsync(
        BookIssue issue);

    Task UpdateBookAsync(
        Book book);

    Task CreateFineAsync(
        Fine fine);

    Task<List<BookIssue>> GetAllIssuesAsync();

    Task<BookIssue?> GetIssueDetailsByUniqueIdAsync(
        string uniqueId);

    Task<List<BookIssue>> GetIssuesByMemberUniqueIdAsync(
        string memberUniqueId);

    Task<List<BookIssue>> GetIssuesByBookUniqueIdAsync(
        string bookUniqueId);

    Task IssueBookAsync(
        string memberUniqueId,
        string bookUniqueId,
        DateTime dueDate,
        string createdby);

    Task ReturnBookAsync(
        string issueUniqueId,
        string updatedBy);
}