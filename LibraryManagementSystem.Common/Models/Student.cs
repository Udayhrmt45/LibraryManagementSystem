namespace LibraryManagementSystem.Common.Models;

public class Student
{
    public int StudentId { get; set; }

    public Guid StudentGuid { get; set; }

    public string StudentName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public DateTime CreatedOn { get; set; }
}