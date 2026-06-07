using LibraryManagementSystem.Common.Models;

namespace LibraryManagementSystem.Store.Abstractions;

public interface IMemberStore
{
    Task<Member> CreateMemberAsync(
        Member member);

    Task<List<Member>> GetAllMembersAsync();

    Task<(List<Member> Members, int TotalRecords)>
        GetMembersPagedAsync(
            int pageNumber,
            int pageSize);

    Task<Member?> GetMemberByUniqueIdAsync(
        string uniqueId);

    Task<bool> EmailExistsAsync(
        string email);

    Task BulkInsertMembersAsync(
    List<BulkMemberRequestDto> members,
    string createdBy);

    Task UpdateMemberAsync(
        Member member);

    Task SoftDeleteMemberAsync(
        Member member);
}