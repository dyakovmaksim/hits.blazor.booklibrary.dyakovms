using BookLibraryApp.Models;

namespace BookLibraryApp.Services;

// делегат + событие (задание)
public delegate void BookBorrowedHandler(Book book, LibraryMember member);

public interface IBookService
{
    event BookBorrowedHandler? BookBorrowed;

    Task<List<Book>> GetAllBooksAsync();
    Task<List<Book>> GetAvailableBooksAsync();
    Task<List<BorrowTransaction>> GetBorrowedByMemberAsync(int memberId);
    Task<List<BookInventoryRow>> GetInventorySummaryAsync();
    Task<Book> AddBookAsync(BookCreateModel model);
    Task DeleteBookAsync(int bookId);
    Task BorrowBookAsync(int bookId, int memberId);
    Task ReturnBookAsync(int transactionId);
}
