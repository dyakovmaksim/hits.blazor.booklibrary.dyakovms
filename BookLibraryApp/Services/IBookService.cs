using BookLibraryApp.Models;

namespace BookLibraryApp.Services;

// делегат + событие (задание)
public delegate void BookBorrowedHandler(Book book, LibraryMember member);

public interface IBookService
{
    event BookBorrowedHandler? BookBorrowed;

    Task<List<Book>> GetAllBooksAsync();
    Task BorrowBookAsync(int bookId, int memberId);
    Task ReturnBookAsync(int transactionId);
}
