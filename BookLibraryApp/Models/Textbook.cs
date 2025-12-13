using BookLibraryApp.Interfaces;

namespace BookLibraryApp.Models;

public class Textbook : Book, IPrintable, ILoggable
{
    // атрибут из варианта :contentReference[oaicite:2]{index=2}
    public string Subject { get; set; } = "";

    // + доп. атрибуты
    public int GradeLevel { get; set; }
    public bool HasPracticeTests { get; set; }
    public string Publisher { get; set; } = "";

    public Textbook() { }

    public Textbook(string title, string author, int year, string isbn, int pages, decimal price,
        string subject, int gradeLevel, bool hasPracticeTests, string publisher)
        : base(title, author, year, isbn, pages, price)
    {
        Subject = subject;
        GradeLevel = gradeLevel;
        HasPracticeTests = hasPracticeTests;
        Publisher = publisher;
    }

    // override (перекрытие) — вариант требует переопределять методы :contentReference[oaicite:3]{index=3}
    public override string Read() => $"Читаем учебник по {Subject}: {Title} (класс: {GradeLevel})";

    public override string GetInfo() =>
        base.GetInfo() + $", Предмет: {Subject}, Класс: {GradeLevel}, Издатель: {Publisher}";

    // интерфейс (обычная реализация)
    public string PrintLabel() => $"[Учебник] {Title} / {Subject}";

    // ЯВНАЯ реализация интерфейса (explicit)
    string ILoggable.Log() => $"LOG(Textbook): {Title}, subject={Subject}, available={IsAvailable}";
}
