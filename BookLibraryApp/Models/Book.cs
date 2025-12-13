using System.ComponentModel.DataAnnotations;

namespace BookLibraryApp.Models;

public abstract class Book
{
    public int Id { get; set; }

    [Required] public string Title { get; set; } = "";
    [Required] public string Author { get; set; } = "";
    public int YearOfPublication { get; set; }

    // + доп. атрибуты (3-4)
    public string Isbn { get; set; } = "";
    public int Pages { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; protected set; } = true;

    // Конструкторы (с использованием геттеров/сеттеров)
    protected Book() { }

    protected Book(string title, string author, int year, string isbn, int pages, decimal price)
    {
        Title = title;
        Author = author;
        YearOfPublication = year;
        Isbn = isbn;
        Pages = pages;
        Price = price;
        IsAvailable = true;
    }

    // Методы из варианта :contentReference[oaicite:1]{index=1}
    public virtual string GetInfo() =>
        $"{Title} — {Author} ({YearOfPublication}), ISBN: {Isbn}, стр: {Pages}, цена: {Price}";

    public virtual string Read() => $"Читаем книгу: {Title}";
    public virtual string Borrow() => IsAvailable ? $"Выдали книгу: {Title}" : $"Книга недоступна: {Title}";

    // Полиморфизм: перегрузка (overload)
    public string Borrow(string borrowerName) => $"{Borrow()} (читатель: {borrowerName})";

    // + доп. методы (3-4)
    public void MarkBorrowed() => IsAvailable = false;
    public void MarkReturned() => IsAvailable = true;

    public virtual decimal ApplyDiscount(decimal percent)
    {
        if (percent < 0 || percent > 100) return Price;
        Price -= Price * (percent / 100m);
        return Price;
    }
}
