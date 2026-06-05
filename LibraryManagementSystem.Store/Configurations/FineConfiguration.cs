using LibraryManagementSystem.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Store.Configurations;

public class FineConfiguration :
    IEntityTypeConfiguration<Fine>
{
    public void Configure(
        EntityTypeBuilder<Fine> builder)
    {
        builder.ToTable("tbl_Fines");

        builder.HasKey(x => x.FineId);

        builder.Property(x => x.UniqueId)
               .HasColumnName("FineUniqueId");

        builder.Property(x => x.IssueId)
               .HasColumnName("IssueId");

        builder.Property(x => x.FineAmount)
               .HasColumnName("FineAmount");

        builder.Property(x => x.IsPaid)
               .HasColumnName("IsPaid");

        builder.Property(x => x.PaidOn)
               .HasColumnName("PaidOn");

        builder.Ignore(x => x.IssueUniqueId);
    }
}