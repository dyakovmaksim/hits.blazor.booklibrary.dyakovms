using System;
using BookLibraryApp.Data;
using BookLibraryApp.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BookLibraryApp.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly SimpleAuthStateProvider _stateProvider;
    private readonly LibrarianOptions _options;

    public AuthService(AppDbContext db, SimpleAuthStateProvider stateProvider, IOptions<LibrarianOptions> options)
    {
        _db = db;
        _stateProvider = stateProvider;
        _options = options.Value;
    }

    public async Task<bool> LoginAsMemberAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(password))
        {
            return false;
        }

        var normalized = email.Trim();
        var member = await _db.Members.FirstOrDefaultAsync(m => m.Email == normalized);

        if (member is null || member.Password != password)
        {
            return false;
        }

        _stateProvider.SetMember(member);
        return true;
    }

    public Task<bool> LoginAsLibrarianAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return Task.FromResult(false);
        }

        var isEmailValid = string.Equals(email.Trim(), _options.Email, StringComparison.OrdinalIgnoreCase);
        var isPasswordValid = password == _options.Password;

        if (isEmailValid && isPasswordValid)
        {
            _stateProvider.SetLibrarian(_options.Email);
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }

    public Task LogoutAsync()
    {
        _stateProvider.Clear();
        return Task.CompletedTask;
    }
}
