using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;

namespace LibraryManagementSystem.Service.Abstractions;

public interface IBookIssueService
{
    //Task<BookIssueResponseDto>
    //    IssueBookAsync(
    //        IssueBookRequestDto request);

    Task IssueBookAsync(
            IssueBookRequestDto request, string createdBy);

    Task ReturnBookAsync(
        ReturnBookRequestDto request);

    Task<List<BookIssueResponseDto>>
    GetAllIssuesAsync();

    Task<BookIssueResponseDto>
        GetIssueByUniqueIdAsync(
            string uniqueId);

    Task<List<BookIssueResponseDto>>
        GetIssuesByMemberAsync(
            string memberUniqueId);

    Task<List<BookIssueResponseDto>>
        GetIssuesByBookAsync(
            string bookUniqueId);
}