USE [LibraryManagementDB]
GO
/****** Object:  Table [dbo].[mst_Authors]    Script Date: 02-06-2026 16:22:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mst_Authors](
	[AuthorId] [int] IDENTITY(1,1) NOT NULL,
	[AuthorGuid] [uniqueidentifier] NOT NULL,
	[AuthorName] [varchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [varchar](100) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](100) NULL,
	[AuthorUniqueId] [varchar](32) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Authors_Name] UNIQUE NONCLUSTERED 
(
	[AuthorName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Authors_UniqueId] UNIQUE NONCLUSTERED 
(
	[AuthorUniqueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mst_Books]    Script Date: 02-06-2026 16:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mst_Books](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[BookGuid] [uniqueidentifier] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
	[ISBN] [varchar](50) NULL,
	[Title] [varchar](300) NOT NULL,
	[TotalCopies] [int] NOT NULL,
	[AvailableCopies] [int] NOT NULL,
	[PublishedYear] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [varchar](100) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](100) NULL,
	[BookUniqueId] [varchar](32) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Books_UniqueId] UNIQUE NONCLUSTERED 
(
	[BookUniqueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mst_Categories]    Script Date: 02-06-2026 16:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mst_Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryGuid] [uniqueidentifier] NOT NULL,
	[CategoryName] [varchar](150) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [varchar](100) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](100) NULL,
	[CategoryUniqueId] [varchar](32) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Categories_Name] UNIQUE NONCLUSTERED 
(
	[CategoryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Categories_UniqueId] UNIQUE NONCLUSTERED 
(
	[CategoryUniqueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mst_Members]    Script Date: 02-06-2026 16:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mst_Members](
	[MemberId] [int] IDENTITY(1,1) NOT NULL,
	[MemberGuid] [uniqueidentifier] NOT NULL,
	[FullName] [varchar](200) NOT NULL,
	[Email] [varchar](200) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[MembershipDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [varchar](100) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](100) NULL,
	[MemberUniqueId] [varchar](32) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Members_UniqueId] UNIQUE NONCLUSTERED 
(
	[MemberUniqueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mst_Roles]    Script Date: 02-06-2026 16:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mst_Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleGuid] [uniqueidentifier] NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [varchar](100) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](100) NULL,
	[RoleUniqueId] [varchar](32) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Roles_UniqueId] UNIQUE NONCLUSTERED 
(
	[RoleUniqueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mst_Users]    Script Date: 02-06-2026 16:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mst_Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserGuid] [uniqueidentifier] NOT NULL,
	[RoleId] [int] NOT NULL,
	[FullName] [varchar](200) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[PasswordHash] [varchar](500) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [varchar](100) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](100) NULL,
	[UserUniqueId] [varchar](32) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Users_UniqueId] UNIQUE NONCLUSTERED 
(
	[UserUniqueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_BookIssues]    Script Date: 02-06-2026 16:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_BookIssues](
	[IssueId] [int] IDENTITY(1,1) NOT NULL,
	[IssueGuid] [uniqueidentifier] NOT NULL,
	[MemberId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
	[IssueDate] [datetime] NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[ReturnDate] [datetime] NULL,
	[Status] [varchar](30) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [varchar](100) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](100) NULL,
	[IssueUniqueId] [varchar](32) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IssueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Issue_UniqueId] UNIQUE NONCLUSTERED 
(
	[IssueUniqueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_ErrorLogs]    Script Date: 02-06-2026 16:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_ErrorLogs](
	[ErrorLogId] [int] IDENTITY(1,1) NOT NULL,
	[ErrorLogGuid] [uniqueidentifier] NOT NULL,
	[ErrorSource] [varchar](100) NULL,
	[ErrorMessage] [nvarchar](max) NULL,
	[ErrorProcedure] [varchar](200) NULL,
	[ErrorLineNumber] [int] NULL,
	[ErrorStackTrace] [nvarchar](max) NULL,
	[RequestPath] [varchar](500) NULL,
	[UserName] [varchar](200) NULL,
	[LogLevel] [varchar](50) NULL,
	[LoggedOn] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [varchar](100) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](100) NULL,
	[ErrorLogUniqueId] [varchar](32) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ErrorLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_ErrorLog_UniqueId] UNIQUE NONCLUSTERED 
(
	[ErrorLogUniqueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Fines]    Script Date: 02-06-2026 16:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Fines](
	[FineId] [int] IDENTITY(1,1) NOT NULL,
	[FineGuid] [uniqueidentifier] NOT NULL,
	[IssueId] [int] NOT NULL,
	[FineAmount] [decimal](10, 2) NOT NULL,
	[IsPaid] [bit] NOT NULL,
	[PaidOn] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [varchar](100) NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](100) NULL,
	[FineUniqueId] [varchar](32) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Fine_UniqueId] UNIQUE NONCLUSTERED 
(
	[FineUniqueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[mst_Authors] ADD  DEFAULT (newid()) FOR [AuthorGuid]
GO
ALTER TABLE [dbo].[mst_Authors] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[mst_Authors] ADD  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[mst_Authors] ADD  CONSTRAINT [DF_AuthorUniqueId]  DEFAULT (replace(CONVERT([varchar](36),newid()),'-','')) FOR [AuthorUniqueId]
GO
ALTER TABLE [dbo].[mst_Books] ADD  DEFAULT (newid()) FOR [BookGuid]
GO
ALTER TABLE [dbo].[mst_Books] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[mst_Books] ADD  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[mst_Books] ADD  CONSTRAINT [DF_BookUniqueId]  DEFAULT (replace(CONVERT([varchar](36),newid()),'-','')) FOR [BookUniqueId]
GO
ALTER TABLE [dbo].[mst_Categories] ADD  DEFAULT (newid()) FOR [CategoryGuid]
GO
ALTER TABLE [dbo].[mst_Categories] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[mst_Categories] ADD  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[mst_Categories] ADD  CONSTRAINT [DF_CategoryUniqueId]  DEFAULT (replace(CONVERT([varchar](36),newid()),'-','')) FOR [CategoryUniqueId]
GO
ALTER TABLE [dbo].[mst_Members] ADD  DEFAULT (newid()) FOR [MemberGuid]
GO
ALTER TABLE [dbo].[mst_Members] ADD  DEFAULT (getutcdate()) FOR [MembershipDate]
GO
ALTER TABLE [dbo].[mst_Members] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[mst_Members] ADD  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[mst_Members] ADD  CONSTRAINT [DF_MemberUniqueId]  DEFAULT (replace(CONVERT([varchar](36),newid()),'-','')) FOR [MemberUniqueId]
GO
ALTER TABLE [dbo].[mst_Roles] ADD  DEFAULT (newid()) FOR [RoleGuid]
GO
ALTER TABLE [dbo].[mst_Roles] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[mst_Roles] ADD  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[mst_Roles] ADD  CONSTRAINT [DF_RoleUniqueId]  DEFAULT (replace(CONVERT([varchar](36),newid()),'-','')) FOR [RoleUniqueId]
GO
ALTER TABLE [dbo].[mst_Users] ADD  DEFAULT (newid()) FOR [UserGuid]
GO
ALTER TABLE [dbo].[mst_Users] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[mst_Users] ADD  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[mst_Users] ADD  CONSTRAINT [DF_UserUniqueId]  DEFAULT (replace(CONVERT([varchar](36),newid()),'-','')) FOR [UserUniqueId]
GO
ALTER TABLE [dbo].[tbl_BookIssues] ADD  DEFAULT (newid()) FOR [IssueGuid]
GO
ALTER TABLE [dbo].[tbl_BookIssues] ADD  DEFAULT (getutcdate()) FOR [IssueDate]
GO
ALTER TABLE [dbo].[tbl_BookIssues] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[tbl_BookIssues] ADD  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[tbl_BookIssues] ADD  CONSTRAINT [DF_IssueUniqueId]  DEFAULT (replace(CONVERT([varchar](36),newid()),'-','')) FOR [IssueUniqueId]
GO
ALTER TABLE [dbo].[tbl_ErrorLogs] ADD  DEFAULT (newid()) FOR [ErrorLogGuid]
GO
ALTER TABLE [dbo].[tbl_ErrorLogs] ADD  DEFAULT (getutcdate()) FOR [LoggedOn]
GO
ALTER TABLE [dbo].[tbl_ErrorLogs] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[tbl_ErrorLogs] ADD  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[tbl_ErrorLogs] ADD  CONSTRAINT [DF_ErrorLogUniqueId]  DEFAULT (replace(CONVERT([varchar](36),newid()),'-','')) FOR [ErrorLogUniqueId]
GO
ALTER TABLE [dbo].[tbl_Fines] ADD  DEFAULT (newid()) FOR [FineGuid]
GO
ALTER TABLE [dbo].[tbl_Fines] ADD  DEFAULT ((0)) FOR [IsPaid]
GO
ALTER TABLE [dbo].[tbl_Fines] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[tbl_Fines] ADD  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[tbl_Fines] ADD  CONSTRAINT [DF_FineUniqueId]  DEFAULT (replace(CONVERT([varchar](36),newid()),'-','')) FOR [FineUniqueId]
GO
ALTER TABLE [dbo].[mst_Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Author] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[mst_Authors] ([AuthorId])
GO
ALTER TABLE [dbo].[mst_Books] CHECK CONSTRAINT [FK_Books_Author]
GO
ALTER TABLE [dbo].[mst_Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[mst_Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[mst_Books] CHECK CONSTRAINT [FK_Books_Category]
GO
ALTER TABLE [dbo].[mst_Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[mst_Roles] ([RoleId])
GO
ALTER TABLE [dbo].[mst_Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
ALTER TABLE [dbo].[tbl_BookIssues]  WITH CHECK ADD  CONSTRAINT [FK_Issue_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[mst_Books] ([BookId])
GO
ALTER TABLE [dbo].[tbl_BookIssues] CHECK CONSTRAINT [FK_Issue_Book]
GO
ALTER TABLE [dbo].[tbl_BookIssues]  WITH CHECK ADD  CONSTRAINT [FK_Issue_Member] FOREIGN KEY([MemberId])
REFERENCES [dbo].[mst_Members] ([MemberId])
GO
ALTER TABLE [dbo].[tbl_BookIssues] CHECK CONSTRAINT [FK_Issue_Member]
GO
ALTER TABLE [dbo].[tbl_Fines]  WITH CHECK ADD  CONSTRAINT [FK_Fines_Issue] FOREIGN KEY([IssueId])
REFERENCES [dbo].[tbl_BookIssues] ([IssueId])
GO
ALTER TABLE [dbo].[tbl_Fines] CHECK CONSTRAINT [FK_Fines_Issue]
GO
