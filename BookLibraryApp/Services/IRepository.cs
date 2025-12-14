namespace BookLibraryApp.Services;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task RemoveAsync(T entity);
    Task SaveAsync();
}
