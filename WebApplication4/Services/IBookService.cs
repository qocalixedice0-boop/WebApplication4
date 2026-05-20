using WebApplication4.DTOs;
using WebApplication4.Models;

namespace WebApplication4.Services
{
    public interface IBookService
    {
        Task<List<BookResponseDto>> GetBooksAsync(bool includeCategory,bool includeAuthors);
        Task<BookResponseDto> GetByIdAsync(Guid id, bool includeCategory,bool includeAuthors);
        Task CreateAsync(CreateBookDto dto);
         Task UpdateAsync(Guid Id,CreateBookDto dto);
         Task DeleteAsync(Guid id);

    }
}
