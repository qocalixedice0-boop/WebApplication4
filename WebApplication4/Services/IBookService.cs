using WebApplication4.DTOs;
using WebApplication4.Models;

namespace WebApplication4.Services
{
    public interface IBookService
    {
        Task<List<BookResponseDto>> GetBooksAsync(bool include);
         Task CreateAsync(CreateBookDto dto);
         Task UpdateAsync(Guid Id,CreateBookDto dto);
         Task DeleteAsync(Guid id);

    }
}
