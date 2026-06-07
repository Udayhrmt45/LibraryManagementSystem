namespace LibraryManagementSystem.Common.Constants;

public static class AppConstants
{
    #region Roles

    public const string AdminRole =
        "Admin";

    public const string LibrarianRole =
        "Librarian";

    #endregion

    #region Messages

    public const string AuthorCreated =
        "Author created successfully.";

    public const string AuthorsRetrived =
        "Authors retrieved successfully";

    public const string AuthorUpdated =
        "Author updated successfully.";

    public const string AuthorDeleted =
        "Author deleted successfully.";

    public const string BookIssued =
        "Book issued successfully.";

    public const string BookReturned =
        "Book returned successfully.";

    public const string MembersInserted =
        "Members inserted successfully.";

    public const string AuthorsInserted =
        "Authors inserted successfully.";

    public const string CategoriesInserted =
        "Categories inserted successfully.";

    public const string BooksInserted =
        "Books inserted successfully.";

    public const string RecordNotFound =
        "Record not found.";

    #endregion

    #region Pagination

    public const int DefaultPageNumber = 1;

    public const int DefaultPageSize = 10;

    #endregion
}