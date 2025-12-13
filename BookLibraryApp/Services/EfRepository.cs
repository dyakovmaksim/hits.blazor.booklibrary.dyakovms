using BookLibraryApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryApp.Services;

public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _db;
    public EfRepository(AppDbContext db) => _db = db;

    public Task<List<T>> GetAllAsync() => _db.Set<T>().ToListAsync();
    public Task<T?> GetByIdAsync(int id) => _db.Set<T>().FindAsync(id).AsTask();
    public async Task AddAsync(T entity) => await _db.Set<T>().AddAsync(entity);
    public Task SaveAsync() => _db.SaveChangesAsync();
}
