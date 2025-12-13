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
}
