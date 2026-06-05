using LibraryManagementSystem.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Store.Configurations;

public class MemberConfiguration :
    IEntityTypeConfiguration<Member>
{
    public void Configure(
        EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("mst_Members");

        builder.HasKey(x => x.MemberId);

        builder.Property(x => x.UniqueId)
               .HasColumnName("MemberUniqueId");

        builder.Property(x => x.FullName)
               .HasColumnName("FullName");

        builder.Property(x => x.Email)
               .HasColumnName("Email");

        builder.Property(x => x.PhoneNumber)
               .HasColumnName("PhoneNumber");

        builder.Property(x => x.MembershipDate)
       .HasColumnName("MembershipDate");

    }
}