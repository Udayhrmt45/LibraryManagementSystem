using LibraryManagementSystem.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Store.Context;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(
        DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Role> Roles { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Book> Books { get; set; }

    public DbSet<Member> Members { get; set; }

    public DbSet<BookIssue> BookIssues { get; set; }

    public DbSet<Fine> Fines { get; set; }

    public DbSet<FineDetailsView> FineDetailsView { get; set; }

    public DbSet<ErrorLog> ErrorLogs { get; set; }

    protected override void OnModelCreating(
    ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(LibraryDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}