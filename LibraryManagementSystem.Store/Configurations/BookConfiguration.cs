using LibraryManagementSystem.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Store.Configurations;

public class BookConfiguration :
    IEntityTypeConfiguration<Book>
{
    public void Configure(
        EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("mst_Books");

        builder.HasKey(x => x.BookId);

        builder.Property(x => x.UniqueId)
               .HasColumnName("BookUniqueId");

        builder.Property(x => x.Title)
               .HasColumnName("Title");

        builder.Property(x => x.ISBN)
               .HasColumnName("ISBN");

        builder.Property(x => x.AuthorId)
               .HasColumnName("AuthorId");

        builder.Property(x => x.CategoryId)
               .HasColumnName("CategoryId");

        builder.Property(x => x.TotalCopies)
               .HasColumnName("TotalCopies");

        builder.Property(x => x.AvailableCopies)
               .HasColumnName("AvailableCopies");

        builder.Property(x => x.PublishedYear)
               .HasColumnName("PublishedYear");

        builder.Ignore(x => x.AuthorName);

        builder.Ignore(x => x.CategoryName);
    }
}