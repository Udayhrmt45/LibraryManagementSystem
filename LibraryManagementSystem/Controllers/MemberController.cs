using LibraryManagementSystem.Common;
using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;
using LibraryManagementSystem.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers;

[Authorize(Roles = "Admin,Librarian")]
[ApiController]
[Route("api/[controller]/[action]")]
/// <summary>
/// Controller for managing library members.
/// </summary>
/// <remarks>
/// Exposes endpoints to create, retrieve, update and delete members. Requires Admin or Librarian role.
/// </remarks>
public class MemberController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MemberController(
        IMemberService memberService)
    {
        _memberService = memberService;
    }

    /// <summary>
    /// Creates a new member.
    /// </summary>
    /// <remarks>
    /// Creates a library member using the supplied CreateMemberRequestDto.
    /// </remarks>
    /// <param name="request">Member creation request containing personal and contact details.</param>
    /// <returns>A CommonResponse wrapping the created MemberResponseDto.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost]
    public async Task<IActionResult> CreateMemberAsync(
            CreateMemberRequestDto request)
    {
        var result =
            await _memberService
                .CreateMemberAsync(request);

        return Ok(
            new CommonResponse<MemberResponseDto>
            {
                Success = true,
                Message = "Member created successfully",
                Data = result
            });
    }

    /// <summary>
    /// Retrieves all members.
    /// </summary>
    /// <remarks>
    /// Returns a list of all registered members in the system.
    /// </remarks>
    /// <returns>A CommonResponse wrapping a list of MemberResponseDto objects.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    public async Task<IActionResult> GetAllMembersAsync()
    {
        var result =
            await _memberService
                .GetAllMembersAsync();

        return Ok(
            new CommonResponse<List<MemberResponseDto>>
            {
                Success = true,
                Message = "Members retrieved successfully",
                Data = result
            });
    }

    /// <summary>
    /// Retrieves members with pagination.
    /// </summary>
    /// <remarks>
    /// Returns a paged list of members including pagination metadata such as page number, page size and total records.
    /// </remarks>
    /// <param name="request">Pagination request containing page number and page size.</param>
    /// <returns>A CommonResponse wrapping a paged response of MemberResponseDto.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    public async Task<IActionResult> GetMembersPagedAsync(
            [FromQuery]
            PaginationRequestDto request)
    {
        var result =
            await _memberService
                .GetMembersPagedAsync(request);

        return Ok(
            new CommonResponse<
                PagedResponseDto<MemberResponseDto>>
            {
                Success = true,
                Message = "Members retrieved successfully",
                Data = result
            });
    }

    /// <summary>
    /// Retrieves a member by unique identifier.
    /// </summary>
    /// <remarks>
    /// Returns member details for the provided uniqueId.
    /// </remarks>
    /// <param name="uniqueId">The unique identifier of the member to retrieve.</param>
    /// <returns>A CommonResponse wrapping the MemberResponseDto.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("{uniqueId}")]
    public async Task<IActionResult> GetMemberByUniqueIdAsync(
            string uniqueId)
    {
        var result =
            await _memberService
                .GetMemberByUniqueIdAsync(
                    uniqueId);

        return Ok(
            new CommonResponse<MemberResponseDto>
            {
                Success = true,
                Message = "Member retrieved successfully",
                Data = result
            });
    }

    /// <summary>
    /// Updates an existing member.
    /// </summary>
    /// <remarks>
    /// Updates member information identified by uniqueId with the supplied update request.
    /// </remarks>
    /// <param name="uniqueId">The unique identifier of the member to update.</param>
    /// <param name="request">The update request containing member fields to change.</param>
    /// <returns>A CommonResponse indicating the update result.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPut("{uniqueId}")]
    public async Task<IActionResult> UpdateMemberAsync(
            string uniqueId,
            UpdateMemberRequestDto request)
    {
        await _memberService
            .UpdateMemberAsync(
                uniqueId,
                request);

        return Ok(
            new CommonResponse<string>
            {
                Success = true,
                Message = "Member updated successfully"
            });
    }

    /// <summary>
    /// Deletes a member.
    /// </summary>
    /// <remarks>
    /// Deletes the member identified by uniqueId from the system.
    /// </remarks>
    /// <param name="uniqueId">The unique identifier of the member to delete.</param>
    /// <returns>A CommonResponse indicating the deletion result.</returns>
    /// <response code="200">Request processed successfully.</response>
    /// <response code="201">Resource created successfully.</response>
    /// <response code="400">Invalid request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="403">Forbidden.</response>
    /// <response code="404">Resource not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpDelete("{uniqueId}")]
    public async Task<IActionResult> DeleteMemberAsync(
            string uniqueId)
    {
        await _memberService
            .DeleteMemberAsync(
                uniqueId);

        return Ok(
            new CommonResponse<string>
            {
                Success = true,
                Message = "Member deleted successfully"
            });
    }
}