using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;

namespace LibraryManagementSystem.Service.Abstractions;

public interface IMemberService
{
    Task<MemberResponseDto> CreateMemberAsync(CreateMemberRequestDto request);

    Task<List<MemberResponseDto>> GetAllMembersAsync();

    Task<PagedResponseDto<MemberResponseDto>> GetMembersPagedAsync(
            PaginationRequestDto request);

    Task<MemberResponseDto> GetMemberByUniqueIdAsync(
            string uniqueId);

    Task BulkInsertMembersAsync(
    BulkInsertMembersRequestDto request);

    Task UpdateMemberAsync(string uniqueId, UpdateMemberRequestDto request);

    Task DeleteMemberAsync(string uniqueId);
}