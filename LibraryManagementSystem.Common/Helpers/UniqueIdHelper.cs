namespace LibraryManagementSystem.Common.Helpers;

public static class UniqueIdHelper
{
    public static string Generate()
    {
        return Guid.NewGuid()
            .ToString("N").ToUpper();
    }
}