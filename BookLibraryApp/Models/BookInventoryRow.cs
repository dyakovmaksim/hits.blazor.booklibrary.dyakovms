namespace BookLibraryApp.Models;

public class BookInventoryRow
{
    public string Type { get; set; } = "";
    public int Total { get; set; }
    public int Available { get; set; }
    public int Borrowed => Total - Available;
}
