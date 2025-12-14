namespace BookLibraryApp.Models;

public class LibraryMember
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";

    // коллекция (задание)
    public List<BorrowTransaction> Transactions { get; set; } = new();

    public LibraryMember() { }

    public LibraryMember(string fullName, string email, string password)
    {
        FullName = fullName;
        Email = email;
        Password = password;
    }
}
