using LibraryManagementSystem.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Store.Configurations;

public class AuthorConfiguration :
    IEntityTypeConfiguration<Author>
{
    public void Configure(
        EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("mst_Authors");

        builder.HasKey(x => x.AuthorId);

        builder.Property(x => x.UniqueId)
               .HasColumnName("AuthorUniqueId");

        builder.HasIndex(x => x.UniqueId)
               .IsUnique();
    }
}