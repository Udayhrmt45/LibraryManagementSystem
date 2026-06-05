using LibraryManagementSystem.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Store.Configurations;

public class CategoryConfiguration :
    IEntityTypeConfiguration<Category>
{
    public void Configure(
        EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("mst_Categories");

        builder.HasKey(x => x.CategoryId);

        builder.Property(x => x.UniqueId)
               .HasColumnName("CategoryUniqueId");

        builder.HasIndex(x => x.UniqueId)
               .IsUnique();
    }
}