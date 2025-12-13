namespace BookLibraryApp.Models;

public class LibraryMember
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";

    // коллекция (задание)
    public List<BorrowTransaction> Transactions { get; set; } = new();

    public LibraryMember() { }

    public LibraryMember(string fullName, string email)
    {
        FullName = fullName;
        Email = email;
    }
}
