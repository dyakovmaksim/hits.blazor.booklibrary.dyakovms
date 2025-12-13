namespace BookLibraryApp.Models;

public class BorrowTransaction
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public Book? Book { get; set; }

    public int MemberId { get; set; }
    public LibraryMember? Member { get; set; }

    public DateTime BorrowedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ReturnedAt { get; set; }

    public bool IsReturned => ReturnedAt != null;

    public void MarkReturned() => ReturnedAt = DateTime.UtcNow;
}
