using LibraryManagementSystem.Common.Models;

public class ErrorLog : BaseModel
{
    public int ErrorLogId { get; set; }

    public Guid ErrorLogGuid { get; set; }

    public string? ErrorSource { get; set; }

    public string? ErrorMessage { get; set; }

    public string? ErrorProcedure { get; set; }

    public int? ErrorLineNumber { get; set; }

    public string? ErrorStackTrace { get; set; }

    public string? RequestPath { get; set; }

    public string? UserName { get; set; }

    public string? LogLevel { get; set; }

    public DateTime LoggedOn { get; set; }
}