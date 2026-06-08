using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class BulkInsertMembersRequestDto
{
    [Required]
    [MinLength(1, ErrorMessage = "At least one item is required.")]
    public List<BulkMemberRequestDto> Members
    {
        get;
        set;
    } = new();
}