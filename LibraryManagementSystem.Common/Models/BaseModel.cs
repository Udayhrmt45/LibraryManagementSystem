namespace LibraryManagementSystem.Common.Models;

public abstract class BaseModel
{
    protected BaseModel()
    {
        UniqueId = Guid.NewGuid()
            .ToString("N")
            .ToUpper();
    }

    public string UniqueId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }
}