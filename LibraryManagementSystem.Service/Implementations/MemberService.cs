using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;
using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Service.Abstractions;
using LibraryManagementSystem.Store.Abstractions;

namespace LibraryManagementSystem.Service.Implementations;

public class MemberService : IMemberService
{
    private readonly IMemberStore _memberStore;

    public MemberService(
        IMemberStore memberStore)
    {
        _memberStore = memberStore;
    }

    public async Task<MemberResponseDto> CreateMemberAsync(
        CreateMemberRequestDto request)
    {
        try
        {
            var emailExists =
                await _memberStore
                    .EmailExistsAsync(
                        request.Email);

            if (emailExists)
            {
                throw new Exception(
                    "Email already exists");
            }

            var member = new Member
            {
                FullName = request.FullName,

                Email = request.Email,

                PhoneNumber = request.PhoneNumber,

                MembershipDate = DateTime.UtcNow,

                IsActive = true,

                CreatedOn = DateTime.UtcNow
            };

            var createdMember =
                await _memberStore
                    .CreateMemberAsync(member);

            return new MemberResponseDto
            {
                UniqueId = createdMember.UniqueId,

                FullName = createdMember.FullName,

                Email = createdMember.Email,

                PhoneNumber = createdMember.PhoneNumber,

                MembershipDate = createdMember.MembershipDate
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(CreateMemberAsync)}: {ex.Message}");
        }
    }

    public async Task<List<MemberResponseDto>> GetAllMembersAsync()
    {
        try
        {
            var members =
                await _memberStore
                    .GetAllMembersAsync();

            return members
                .Select(member =>
                new MemberResponseDto
                {
                    UniqueId = member.UniqueId,

                    FullName = member.FullName,

                    Email = member.Email,

                    PhoneNumber = member.PhoneNumber,

                    MembershipDate = member.MembershipDate
                })
                .ToList();
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetAllMembersAsync)}: {ex.Message}");
        }
    }

    public async Task<PagedResponseDto<MemberResponseDto>> GetMembersPagedAsync(
            PaginationRequestDto request)
    {
        try
        {
            var result =
                await _memberStore
                    .GetMembersPagedAsync(
                        request.PageNumber,
                        request.PageSize);

            return new PagedResponseDto<MemberResponseDto>
            {
                PageNumber = request.PageNumber,

                PageSize = request.PageSize,

                TotalRecords = result.TotalRecords,

                TotalPages =
                    (int)Math.Ceiling(
                        (double)result.TotalRecords /
                        request.PageSize),

                Data = result.Members
                .Select(member =>
                    new MemberResponseDto
                    {
                        UniqueId = member.UniqueId,

                        FullName = member.FullName,

                        Email = member.Email,

                        PhoneNumber = member.PhoneNumber,

                        MembershipDate = member.MembershipDate
                    }).ToList()
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetMembersPagedAsync)}: {ex.Message}");
        }
    }

    public async Task<MemberResponseDto>
        GetMemberByUniqueIdAsync(
            string uniqueId)
    {
        try
        {
            var member =
                await _memberStore
                    .GetMemberByUniqueIdAsync(
                        uniqueId);

            if (member == null)
            {
                throw new Exception(
                    "Member not found");
            }

            return new MemberResponseDto
            {
                UniqueId = member.UniqueId,

                FullName = member.FullName,

                Email = member.Email,

                PhoneNumber = member.PhoneNumber,

                MembershipDate = member.MembershipDate
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetMemberByUniqueIdAsync)}: {ex.Message}");
        }
    }

    public async Task BulkInsertMembersAsync(
    BulkInsertMembersRequestDto request)
    {
        await _memberStore
            .BulkInsertMembersAsync(
                request.Members,
                "System");
    }

    public async Task UpdateMemberAsync(
        string uniqueId,
        UpdateMemberRequestDto request)
    {
        try
        {
            var member =
                await _memberStore
                    .GetMemberByUniqueIdAsync(
                        uniqueId);

            if (member == null)
            {
                throw new Exception(
                    "Member not found");
            }

            if (!member.Email.Equals(
                    request.Email,
                    StringComparison.OrdinalIgnoreCase))
            {
                var emailExists =
                    await _memberStore
                        .EmailExistsAsync(
                            request.Email);

                if (emailExists)
                {
                    throw new Exception(
                        "Email already exists");
                }
            }

            member.FullName = request.FullName;

            member.Email = request.Email;

            member.PhoneNumber = request.PhoneNumber;

            member.UpdatedOn = DateTime.UtcNow;

            await _memberStore
                .UpdateMemberAsync(member);
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(UpdateMemberAsync)}: {ex.Message}");
        }
    }

    public async Task DeleteMemberAsync(
        string uniqueId)
    {
        try
        {
            var member =
                await _memberStore
                    .GetMemberByUniqueIdAsync(
                        uniqueId);

            if (member == null)
            {
                throw new Exception(
                    "Member not found");
            }

            await _memberStore
                .SoftDeleteMemberAsync(member);
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(DeleteMemberAsync)}: {ex.Message}");
        }
    }
}