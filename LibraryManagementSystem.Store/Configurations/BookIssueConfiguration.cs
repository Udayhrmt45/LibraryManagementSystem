using LibraryManagementSystem.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Store.Configurations;

public class BookIssueConfiguration :
    IEntityTypeConfiguration<BookIssue>
{
    public void Configure(
        EntityTypeBuilder<BookIssue> builder)
    {
        builder.ToTable("tbl_BookIssues");

        builder.HasKey(x => x.IssueId);

        builder.Property(x => x.UniqueId)
               .HasColumnName("IssueUniqueId");

        builder.Property(x => x.MemberId)
               .HasColumnName("MemberId");

        builder.Property(x => x.BookId)
               .HasColumnName("BookId");

        builder.Property(x => x.IssueDate)
               .HasColumnName("IssueDate");

        builder.Property(x => x.DueDate)
               .HasColumnName("DueDate");

        builder.Property(x => x.ReturnDate)
               .HasColumnName("ReturnDate");

        builder.Property(x => x.Status)
               .HasColumnName("Status");

        builder.Ignore(x => x.MemberName);

        builder.Ignore(x => x.BookTitle);
    }
}