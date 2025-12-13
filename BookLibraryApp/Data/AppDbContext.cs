using BookLibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Textbook> Textbooks => Set<Textbook>();
    public DbSet<Fiction> Fictions => Set<Fiction>();
    public DbSet<ScientificLiterature> ScientificLiteratures => Set<ScientificLiterature>();

    public DbSet<LibraryMember> Members => Set<LibraryMember>();
    public DbSet<BorrowTransaction> Transactions => Set<BorrowTransaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TPH-наследование для Book (по умолчанию EF так и сделает, но фиксируем дискриминатор)
        modelBuilder.Entity<Book>()
            .HasDiscriminator<string>("BookType")
            .HasValue<Textbook>("Textbook")
            .HasValue<Fiction>("Fiction")
            .HasValue<ScientificLiterature>("ScientificLiterature");
    }
}
