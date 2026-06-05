-- Indexes

-- Indexes for performance optimization
use LibraryManagementDB;

CREATE UNIQUE NONCLUSTERED INDEX IX_Roles_UniqueId
ON mst_Roles(RoleUniqueId);

CREATE UNIQUE NONCLUSTERED INDEX IX_Users_UniqueId
ON mst_Users(UserUniqueId);

CREATE UNIQUE NONCLUSTERED INDEX IX_Authors_UniqueId
ON mst_Authors(AuthorUniqueId);

CREATE UNIQUE NONCLUSTERED INDEX IX_Categories_UniqueId
ON mst_Categories(CategoryUniqueId);

CREATE UNIQUE NONCLUSTERED INDEX IX_Books_UniqueId
ON mst_Books(BookUniqueId);

CREATE UNIQUE NONCLUSTERED INDEX IX_Members_UniqueId
ON mst_Members(MemberUniqueId);

CREATE UNIQUE NONCLUSTERED INDEX IX_Issues_UniqueId
ON tbl_BookIssues(IssueUniqueId);

CREATE UNIQUE NONCLUSTERED INDEX IX_Fines_UniqueId
ON tbl_Fines(FineUniqueId);


CREATE UNIQUE NONCLUSTERED INDEX IX_Users_Email
ON mst_Users(Email);

CREATE UNIQUE NONCLUSTERED INDEX IX_Members_Email
ON mst_Members(Email);

CREATE UNIQUE NONCLUSTERED INDEX IX_Books_ISBN
ON mst_Books(ISBN);

CREATE NONCLUSTERED INDEX IX_Books_AuthorId
ON mst_Books(AuthorId);

CREATE NONCLUSTERED INDEX IX_Books_CategoryId
ON mst_Books(CategoryId);

CREATE NONCLUSTERED INDEX IX_BookIssues_BookId
ON tbl_BookIssues(BookId);

CREATE NONCLUSTERED INDEX IX_BookIssues_MemberId
ON tbl_BookIssues(MemberId);

CREATE NONCLUSTERED INDEX IX_Fines_IssueId
ON tbl_Fines(IssueId);

CREATE NONCLUSTERED INDEX IX_BookIssues_Status
ON tbl_BookIssues(Status);