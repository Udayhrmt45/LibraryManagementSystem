using LibraryManagementSystem.Common.DTOs.Requests;
using LibraryManagementSystem.Common.DTOs.Responses;

namespace LibraryManagementSystem.Service.Abstractions;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(
        LoginRequestDto request);

}