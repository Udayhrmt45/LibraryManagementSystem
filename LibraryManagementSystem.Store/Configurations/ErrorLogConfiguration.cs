using LibraryManagementSystem.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Store.Configurations;

public class ErrorLogConfiguration
    : IEntityTypeConfiguration<ErrorLog>
{
    public void Configure(
        EntityTypeBuilder<ErrorLog> builder)
    {
        builder.ToTable("tbl_ErrorLogs");

        builder.HasKey(x => x.ErrorLogId);

        builder.Property(x => x.ErrorLogId)
               .HasColumnName("ErrorLogId");

        builder.Property(x => x.ErrorLogGuid)
               .HasColumnName("ErrorLogGuid");

        builder.Property(x => x.UniqueId)
               .HasColumnName("ErrorLogUniqueId");

        builder.Property(x => x.ErrorSource)
               .HasColumnName("ErrorSource");

        builder.Property(x => x.ErrorMessage)
               .HasColumnName("ErrorMessage");

        builder.Property(x => x.ErrorProcedure)
               .HasColumnName("ErrorProcedure");

        builder.Property(x => x.ErrorLineNumber)
               .HasColumnName("ErrorLineNumber");

        builder.Property(x => x.ErrorStackTrace)
               .HasColumnName("ErrorStackTrace");

        builder.Property(x => x.RequestPath)
               .HasColumnName("RequestPath");

        builder.Property(x => x.UserName)
               .HasColumnName("UserName");

        builder.Property(x => x.LogLevel)
               .HasColumnName("LogLevel");

        builder.Property(x => x.LoggedOn)
               .HasColumnName("LoggedOn");

        builder.Property(x => x.IsActive)
               .HasColumnName("IsActive");

        builder.Property(x => x.CreatedOn)
               .HasColumnName("CreatedOn");

        builder.Property(x => x.CreatedBy)
               .HasColumnName("CreatedBy");

        builder.Property(x => x.UpdatedOn)
               .HasColumnName("UpdatedOn");

        builder.Property(x => x.UpdatedBy)
               .HasColumnName("UpdatedBy");
    }
}