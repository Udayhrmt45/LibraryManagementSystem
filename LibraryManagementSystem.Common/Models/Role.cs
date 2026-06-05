namespace LibraryManagementSystem.Common.Models;

public class Role : BaseModel
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = string.Empty;
}