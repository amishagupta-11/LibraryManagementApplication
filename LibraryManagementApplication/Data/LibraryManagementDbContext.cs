using LibraryManagementApplication.Models;
using Microsoft.EntityFrameworkCore;
namespace LibraryManagementApplication.Data;
public class LibraryManagementDbContext : DbContext
{
    /// <summary>
    /// Represents the database context for the Library Management Application.
    ///Inherits from DbContext to provide database interaction capabilities.
    /// </summary>
    /// <param name="options"></param>
    public LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options) : base(options)
    {
    }

    // DbSet properties
    public DbSet<UserInfo> Users { get; set; }
    public DbSet<BookDetails> Books { get; set; }
}
