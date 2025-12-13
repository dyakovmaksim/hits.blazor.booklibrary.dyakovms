using BookLibraryApp.Models;

namespace BookLibraryApp.Interfaces;

public interface IBorrowable
{
    bool CanBorrow(Book book);
}
