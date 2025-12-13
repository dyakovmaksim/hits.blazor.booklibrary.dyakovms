using BookLibraryApp.Interfaces;

namespace BookLibraryApp.Models;

public class Fiction : Book, IPrintable
{
    // атрибут из варианта :contentReference[oaicite:4]{index=4}
    public string Genre { get; set; } = "";

    // + доп. атрибуты
    public string Series { get; set; } = "";
    public int? SeriesNumber { get; set; }
    public string AgeRating { get; set; } = "12+";

    public Fiction() { }

    public Fiction(string title, string author, int year, string isbn, int pages, decimal price,
        string genre, string series, int? seriesNumber, string ageRating)
        : base(title, author, year, isbn, pages, price)
    {
        Genre = genre;
        Series = series;
        SeriesNumber = seriesNumber;
        AgeRating = ageRating;
    }

    // переопределяем Borrow() как в задании :contentReference[oaicite:5]{index=5}
    public override string Borrow()
        => base.Borrow() + (Genre.Length > 0 ? $" (жанр: {Genre})" : "");

    public override string GetInfo() =>
        base.GetInfo() + $", Жанр: {Genre}, Серия: {Series} #{SeriesNumber}, Возраст: {AgeRating}";

    public string PrintLabel() => $"[Худ.лит] {Title} / {Genre}";
}
