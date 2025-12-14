namespace BookLibraryApp.Services;

public interface IAuthService
{
    Task<bool> LoginAsMemberAsync(string email, string password);
    Task<bool> LoginAsLibrarianAsync(string email, string password);
    Task LogoutAsync();
}
