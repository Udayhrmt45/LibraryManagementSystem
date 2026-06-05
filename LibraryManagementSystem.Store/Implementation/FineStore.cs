using LibraryManagementSystem.Common.Models;
using LibraryManagementSystem.Store.Abstractions;
using LibraryManagementSystem.Store.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Store.Implementations;

public class FineStore : IFineStore
{
    private readonly LibraryDbContext _context;

    public FineStore(
        LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Fine>>
        GetAllFinesAsync()
    {
        try
        {
            return await (
                from f in _context.Fines
                join i in _context.BookIssues
                    on f.IssueId equals i.IssueId
                where f.IsActive
                select new Fine
                {
                    FineId = f.FineId,

                    UniqueId = f.UniqueId,

                    FineAmount = f.FineAmount,

                    IsPaid = f.IsPaid,

                    PaidOn = f.PaidOn,

                    IssueUniqueId = i.UniqueId
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetAllFinesAsync)}: {ex.Message}");
        }
    }

    public async Task<Fine?>GetFineByUniqueIdAsync(string uniqueId)
    {
        try
        {
            return await _context.Fines
                .FirstOrDefaultAsync(x =>
                    x.UniqueId == uniqueId &&
                    x.IsActive);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetFineByUniqueIdAsync)}: {ex.Message}");
        }
    }

    public async Task<decimal> CalculateFineAsync(DateTime dueDate, DateTime returnDate)
    {
        try
        {
            var sql =
                @"SELECT dbo.fn_CalculateFine
          (@DueDate,@ReturnDate)";

            var connection =
                _context.Database
                    .GetDbConnection();

            await connection.OpenAsync();

            using var command =
                connection.CreateCommand();

            command.CommandText = sql;

            var dueDateParameter =
                command.CreateParameter();

            dueDateParameter.ParameterName =
                "@DueDate";

            dueDateParameter.Value = dueDate;

            command.Parameters.Add(
                dueDateParameter);

            var returnDateParameter =
                command.CreateParameter();

            returnDateParameter.ParameterName =
                "@ReturnDate";

            returnDateParameter.Value =
                returnDate;

            command.Parameters.Add(
                returnDateParameter);

            var result =
                await command.ExecuteScalarAsync();

            return Convert.ToDecimal(result);
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(CalculateFineAsync)}: {ex.Message}");
        }
    }

    public async Task UpdateFineAsync(
        Fine fine)
    {
        try
        {
            _context.Fines.Update(fine);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(UpdateFineAsync)}: {ex.Message}");
        }
    }

    public async Task<List<FineDetailsView>> GetFineDetailsAsync()
    {
        try
        {
            return await _context.FineDetailsView
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(GetFineDetailsAsync)}: {ex.Message}");
        }
    }
}