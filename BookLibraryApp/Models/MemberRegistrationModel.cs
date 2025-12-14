using System.ComponentModel.DataAnnotations;

namespace BookLibraryApp.Models;

public class MemberRegistrationModel
{
    [Required(ErrorMessage = "Введите имя полностью")]
    [StringLength(100, ErrorMessage = "Имя не должно превышать 100 символов")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите email")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите пароль")]
    [StringLength(60, MinimumLength = 4, ErrorMessage = "Пароль должен быть от 4 до 60 символов")]
    public string Password { get; set; } = string.Empty;
}
