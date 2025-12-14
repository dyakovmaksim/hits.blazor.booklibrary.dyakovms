using System.Security.Claims;
using BookLibraryApp.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookLibraryApp.Services;

public class SimpleAuthStateProvider : AuthenticationStateProvider
{
    public const string MemberRole = "Member";
    public const string LibrarianRole = "Librarian";

    private static readonly ClaimsPrincipal Anonymous = new(new ClaimsIdentity());
    private ClaimsPrincipal _currentUser = Anonymous;

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
        => Task.FromResult(new AuthenticationState(_currentUser));

    public void SetMember(LibraryMember member)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, member.FullName),
            new Claim(ClaimTypes.Email, member.Email),
            new Claim(ClaimTypes.Role, MemberRole),
            new Claim("MemberId", member.Id.ToString())
        };

        SetPrincipal(claims);
    }

    public void SetLibrarian(string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "Библиотекарь"),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, LibrarianRole)
        };

        SetPrincipal(claims);
    }

    public void Clear()
    {
        _currentUser = Anonymous;
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
    }

    private void SetPrincipal(IEnumerable<Claim> claims)
    {
        var identity = new ClaimsIdentity(claims, nameof(SimpleAuthStateProvider));
        _currentUser = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
    }
}
