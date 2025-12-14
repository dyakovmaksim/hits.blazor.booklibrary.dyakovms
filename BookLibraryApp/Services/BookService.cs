using System;
using BookLibraryApp.Data;
using BookLibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApp.Services;

public class BookService : IBookService
{
    private readonly AppDbContext _db;

    public event BookBorrowedHandler? BookBorrowed;

    public BookService(AppDbContext db) => _db = db;

    public Task<List<Book>> GetAllBooksAsync()
        => _db.Books.AsNoTracking().ToListAsync();

    public Task<List<Book>> GetAvailableBooksAsync()
        => _db.Books.AsNoTracking().Where(b => b.IsAvailable).ToListAsync();

    public Task<List<BorrowTransaction>> GetBorrowedByMemberAsync(int memberId)
        => _db.Transactions
            .Include(t => t.Book)
            .AsNoTracking()
            .Where(t => t.MemberId == memberId && t.ReturnedAt == null)
            .OrderByDescending(t => t.BorrowedAt)
            .ToListAsync();

    public async Task<List<BookInventoryRow>> GetInventorySummaryAsync()
    {
        var rows = await _db.Books.AsNoTracking()
            .GroupBy(b => EF.Property<string>(b, "BookType"))
            .Select(g => new BookInventoryRow
            {
                Type = g.Key ?? "Неизвестно",
                Total = g.Count(),
                Available = g.Count(b => b.IsAvailable)
            })
            .OrderBy(r => r.Type)
            .ToListAsync();

        var total = new BookInventoryRow
        {
            Type = "Всего",
            Total = rows.Sum(r => r.Total),
            Available = rows.Sum(r => r.Available)
        };

        rows.Add(total);
        return rows;
    }

    public async Task<Book> AddBookAsync(BookCreateModel model)
    {
        var entity = CreateBook(model);
        _db.Books.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteBookAsync(int bookId)
    {
        var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == bookId);
        if (book is null) return;

        var relatedTransactions = await _db.Transactions.Where(t => t.BookId == bookId).ToListAsync();
        if (relatedTransactions.Count > 0)
        {
            _db.Transactions.RemoveRange(relatedTransactions);
        }

        _db.Books.Remove(book);
        await _db.SaveChangesAsync();
    }

    public async Task BorrowBookAsync(int bookId, int memberId)
    {
        var book = await _db.Books.FirstAsync(x => x.Id == bookId);
        var member = await _db.Members.FirstAsync(x => x.Id == memberId);

        if (!book.IsAvailable) return;

        book.MarkBorrowed();

        var tx = new BorrowTransaction
        {
            BookId = bookId,
            MemberId = memberId,
            BorrowedAt = DateTime.UtcNow
        };

        _db.Transactions.Add(tx);
        await _db.SaveChangesAsync();

        // событие
        BookBorrowed?.Invoke(book, member);
    }

    public async Task ReturnBookAsync(int transactionId)
    {
        var tx = await _db.Transactions.Include(t => t.Book).FirstAsync(t => t.Id == transactionId);
        if (tx.Book is null || tx.IsReturned) return;

        tx.MarkReturned();
        tx.Book.MarkReturned();

        await _db.SaveChangesAsync();
    }

    private static Book CreateBook(BookCreateModel model)
    {
        var isbn = $"AUTO-{Guid.NewGuid():N}".Substring(0, 13);
        const int defaultPages = 120;
        const decimal defaultPrice = 0;

        var title = model.Title.Trim();
        var author = model.Author.Trim();
        var year = model.YearOfPublication;

        return model.Type switch
        {
            BookType.Textbook => new Textbook(
                title,
                author,
                year,
                isbn,
                defaultPages,
                defaultPrice,
                string.IsNullOrWhiteSpace(model.Subject) ? "Общий курс" : model.Subject,
                1,
                false,
                "Авто"),
            BookType.Fiction => new Fiction(
                title,
                author,
                year,
                isbn,
                defaultPages,
                defaultPrice,
                string.IsNullOrWhiteSpace(model.Genre) ? "Жанр" : model.Genre,
                "Серия",
                1,
                "12+"),
            BookType.Scientific => new ScientificLiterature(
                title,
                author,
                year,
                isbn,
                defaultPages,
                defaultPrice,
                string.IsNullOrWhiteSpace(model.FieldOfScience) ? "Общая наука" : model.FieldOfScience,
                true,
                $"10.0000/{Guid.NewGuid():N}".Substring(0, 16),
                0),
            _ => throw new ArgumentOutOfRangeException(nameof(model.Type))
        };
    }
}
