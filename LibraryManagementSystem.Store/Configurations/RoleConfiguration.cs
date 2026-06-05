using LibraryManagementSystem.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Store.Configurations;

public class RoleConfiguration :
    IEntityTypeConfiguration<Role>
{
    public void Configure(
        EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("mst_Roles");

        builder.HasKey(x => x.RoleId);

        builder.Property(x => x.UniqueId)
               .HasColumnName("RoleUniqueId");

        builder.Property(x => x.RoleName)
               .HasMaxLength(100);

        builder.HasIndex(x => x.UniqueId)
               .IsUnique();
    }
}