using BookLibraryApp.Models;

namespace BookLibraryApp.Data;

public static class SeedData
{
    public static void AddInitialData(AppDbContext db)
    {
        if (db.Books.Any()) return;

        db.Members.Add(new LibraryMember("Иван Петров", "ivan@example.com"));

        db.Books.AddRange(
            new Textbook("Алгебра", "Иванов", 2020, "111-111", 300, 500,
                "Математика", 9, true, "Просвещение"),
            new Fiction("Метро 2033", "Глуховский", 2005, "222-222", 400, 700,
                "Постапокалипсис", "Метро", 1, "16+"),
            new ScientificLiterature("Основы ML", "Сидоров", 2022, "333-333", 250, 1200,
                "Информатика", true, "10.1234/example", 15)
        );

        db.SaveChanges();
    }
}
