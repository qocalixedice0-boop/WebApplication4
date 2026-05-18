using WebApplication4.Models;

namespace WebApplication4.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetBooksAsync(bool category, bool authors);
    Task<Book> GetByIdAsync(Guid id);
    Task CreateAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(Guid id);
}

