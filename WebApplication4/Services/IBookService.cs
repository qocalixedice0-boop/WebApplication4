using WebApplication4.DTOs;
using WebApplication4.Models;

namespace WebApplication4.Services
{
    public interface IBookService
    {
         Task<List<BookDto>> GetBooksAsync();
         Task CreateAsync(BookDto dto);
         Task UpdateAsync(Guid Id,BookDto dto);
         Task DeleteAsync(Guid id);

    }
}
