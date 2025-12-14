using System.ComponentModel.DataAnnotations;

namespace BookLibraryApp.Models;

public class BookCreateModel
{
    [Required]
    public BookType Type { get; set; } = BookType.Textbook;

    [Required(ErrorMessage = "Введите название")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите автора")]
    public string Author { get; set; } = string.Empty;

    [Range(1500, 2100, ErrorMessage = "Год публикации некорректен")]
    public int YearOfPublication { get; set; } = DateTime.UtcNow.Year;

    public string Subject { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string FieldOfScience { get; set; } = string.Empty;
}
