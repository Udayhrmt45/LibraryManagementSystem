using LibraryManagementSystem.Store.Context;

public class ErrorLogStore
    : IErrorLogStore
{
    private readonly LibraryDbContext _context;

    public ErrorLogStore(
        LibraryDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(
        ErrorLog errorLog)
    {
        try
        {
            await _context.ErrorLogs
                .AddAsync(errorLog);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Store Error - {nameof(CreateAsync)}: {ex.Message}");
        }
    }
}