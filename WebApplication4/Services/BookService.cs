using WebApplication4.DTOs;
using WebApplication4.Models;
using WebApplication4.Repositories;

namespace WebApplication4.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;
        public BookService(IBookRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<BookDto>> GetBooksAsync()
        {
            var books =await _repo.GetBooksAsync();
            return books.Select(b => new BookDto
            {
                Title = b.Title,
                Author = b.Author
            }).ToList();
        }
        public async Task CreateAsync(BookDto dto)
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Author = dto.Author
            };
            await _repo.CreateAsync(book);
        }
        public async Task UpdateAsync(Guid id, BookDto dto)
        {
            if (dto == null) return;
            var book = new Book
            {
                Id = id,
                Title = dto.Title,
                Author = dto.Author
            };
            await _repo.UpdateAsync(book);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
