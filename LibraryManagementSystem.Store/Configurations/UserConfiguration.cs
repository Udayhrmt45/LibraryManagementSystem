using LibraryManagementSystem.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Store.Configurations;

public class UserConfiguration :
    IEntityTypeConfiguration<User>
{
    public void Configure(
        EntityTypeBuilder<User> builder)
    {
        builder.ToTable("mst_Users");

        builder.HasKey(x => x.UserId);

        builder.Property(x => x.UniqueId)
            .HasColumnName("UserUniqueId");

        builder.HasIndex(x => x.UniqueId)
               .IsUnique();

        builder.Ignore(x => x.RoleName);

        builder.HasOne<Role>()
               .WithMany()  
               .HasForeignKey(x => x.RoleId);
    }
}