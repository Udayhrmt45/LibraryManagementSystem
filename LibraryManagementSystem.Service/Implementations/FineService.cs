using LibraryManagementSystem.Common.DTOs.Responses;
using LibraryManagementSystem.Service.Abstractions;
using LibraryManagementSystem.Store.Abstractions;

namespace LibraryManagementSystem.Service.Implementations;

public class FineService : IFineService
{
    private readonly IFineStore _fineStore;

    public FineService(
        IFineStore fineStore)
    {
        _fineStore = fineStore;
    }

    public async Task<List<FineResponseDto>>
        GetAllFinesAsync()
    {
        try
        {
            var fines =
                await _fineStore
                    .GetAllFinesAsync();

            return fines.Select(fine =>
                new FineResponseDto
                {
                    UniqueId = fine.UniqueId,

                    FineAmount = fine.FineAmount,

                    IsPaid = fine.IsPaid,

                    PaidOn = fine.PaidOn,

                    IssueUniqueId =
                        fine.IssueUniqueId
                })
                .ToList();
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetAllFinesAsync)}: {ex.Message}");
        }
    }

    public async Task<FineResponseDto>
        GetFineByUniqueIdAsync(
            string uniqueId)
    {
        try
        {
            var fine =
                await _fineStore
                    .GetFineByUniqueIdAsync(
                        uniqueId);

            if (fine == null)
            {
                throw new Exception(
                    "Fine not found");
            }

            return new FineResponseDto
            {
                UniqueId = fine.UniqueId,

                FineAmount = fine.FineAmount,

                IsPaid = fine.IsPaid,

                PaidOn = fine.PaidOn
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(GetFineByUniqueIdAsync)}: {ex.Message}");
        }
    }

    public async Task < List < FineDetailsResponseDto >>
    GetFineDetailsAsync()
    {
    try
    {
        var fineDetails =
            await _fineStore
                .GetFineDetailsAsync();

        return fineDetails
            .Select(x =>
                new FineDetailsResponseDto
                {
            FineUniqueId =
                        x.FineUniqueId,

                    MemberUniqueId =
                        x.MemberUniqueId,

                    MemberName =
                        x.FullName,

                    BookUniqueId =
                        x.BookUniqueId,

                    BookTitle =
                        x.Title,

                    FineAmount =
                        x.FineAmount,

                    IsPaid =
                        x.IsPaid,

                    CreatedOn =
                        x.CreatedOn,

                    PaidOn =
                        x.PaidOn
        })
            .ToList();
    }
    catch (Exception ex)
    {
        throw new Exception($"Service Error - {nameof(GetFineDetailsAsync)}: {ex.Message}");
    }
    }

    public async Task<decimal> CalculateFineAsync(DateTime dueDate, DateTime returnDate)
    {
        try
        {
            return await _fineStore
                .CalculateFineAsync(
                    dueDate,
                    returnDate);
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(CalculateFineAsync)}: {ex.Message}");
        }
    }


    public async Task PayFineAsync(
        string uniqueId)
    {
        try
        {
            var fine =
                await _fineStore
                    .GetFineByUniqueIdAsync(
                        uniqueId);

            if (fine == null)
            {
                throw new Exception(
                    "Fine not found");
            }

            if (fine.IsPaid)
            {
                throw new Exception(
                    "Fine already paid");
            }

            fine.IsPaid = true;

            fine.PaidOn =
                DateTime.UtcNow;

            fine.UpdatedOn =
                DateTime.UtcNow;

            await _fineStore
                .UpdateFineAsync(
                    fine);
        }
        catch (Exception ex)
        {
            throw new Exception($"Service Error - {nameof(PayFineAsync)}: {ex.Message}");
        }
    }
}