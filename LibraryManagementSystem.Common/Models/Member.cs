namespace LibraryManagementSystem.Common.Models;

public class Member : BaseModel
{
    public int MemberId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime MembershipDate { get; set; }
}