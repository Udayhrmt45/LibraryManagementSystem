using LibraryManagementSystem.Common.DTOs.Responses;

namespace LibraryManagementSystem.Service.Abstractions;

public interface IFineService
{
    Task<List<FineResponseDto>>
        GetAllFinesAsync();

    Task<FineResponseDto>
        GetFineByUniqueIdAsync(
            string uniqueId);

    Task<List<FineDetailsResponseDto>>
    GetFineDetailsAsync();

    Task<decimal> CalculateFineAsync(DateTime dueDate, DateTime returnDate);

    Task PayFineAsync(
        string uniqueId);
}