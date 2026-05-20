using WebApplication4.DTOs;
using WebApplication4.Models;

namespace WebApplication4.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetBooksAsync(bool includeCategory,bool includeAuthors);
    Task<Book> GetByIdAsync(Guid id, bool includeCategory,bool includeAuthors);
    Task CreateAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(Book book);
}

