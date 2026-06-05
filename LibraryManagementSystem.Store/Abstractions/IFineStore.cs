using LibraryManagementSystem.Common.Models;

namespace LibraryManagementSystem.Store.Abstractions;

public interface IFineStore
{
    Task<List<Fine>> GetAllFinesAsync();

    Task<Fine?> GetFineByUniqueIdAsync(string uniqueId);

    Task<List<FineDetailsView>> GetFineDetailsAsync();

    Task<decimal> CalculateFineAsync(DateTime dueDate,DateTime returnDate);

    Task UpdateFineAsync(Fine fine);
}