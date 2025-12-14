using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApp.Data;

public static class DbMaintenance
{
    public static void EnsureMemberPasswordColumn(AppDbContext db)
    {
        if (!ColumnExists(db, "Members", "Password"))
        {
            db.Database.ExecuteSqlRaw("ALTER TABLE Members ADD COLUMN Password TEXT NOT NULL DEFAULT ''");
        }

        NormalizeEmptyPasswords(db);
    }

    private static bool ColumnExists(AppDbContext db, string tableName, string columnName)
    {
        using var connection = db.Database.GetDbConnection();
        if (connection.State != System.Data.ConnectionState.Open)
        {
            connection.Open();
        }

        using var command = connection.CreateCommand();
        command.CommandText = $"PRAGMA table_info('{tableName}')";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var nameOrdinal = reader.GetOrdinal("name");
            var name = reader.GetString(nameOrdinal);

            if (string.Equals(name, columnName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    private static void NormalizeEmptyPasswords(AppDbContext db)
    {
        db.Database.ExecuteSqlRaw("UPDATE Members SET Password = 'reader123' WHERE Password IS NULL OR Password = ''");
    }
}
