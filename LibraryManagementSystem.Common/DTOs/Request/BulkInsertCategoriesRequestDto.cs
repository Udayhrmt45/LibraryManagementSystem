using LibraryManagementSystem.Common.DTOs.Requests;

public class BulkInsertCategoriesRequestDto
{
    public List<CreateCategoryRequestDto> Categories {get; set;} = new();
}