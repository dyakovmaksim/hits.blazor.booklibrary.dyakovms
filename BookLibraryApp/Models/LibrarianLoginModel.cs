using System.ComponentModel.DataAnnotations;

namespace BookLibraryApp.Models;

public class LibrarianLoginModel
{
    [Required(ErrorMessage = "Введите email")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите пароль")]
    [StringLength(60, MinimumLength = 3, ErrorMessage = "Пароль должен быть от 3 до 60 символов")]
    public string Password { get; set; } = string.Empty;
}
