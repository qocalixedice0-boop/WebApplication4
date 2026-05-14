using WebApplication4.DTOs;
using WebApplication4.Models;
using WebApplication4.Repositories;

namespace WebApplication4.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IAuthorRepository _authorRepo;
        public BookService(IBookRepository bookRepo, ICategoryRepository categoryRepo, IAuthorRepository authorRepo)
        {
            _bookRepo = bookRepo;
            _categoryRepo = categoryRepo;
            _authorRepo = authorRepo;
        }
        public async Task<List<BookDto>> GetBooksAsync()
        {
            var books = await _bookRepo.GetBooksAsync();
            return books.Select(b => new BookDto
            {
                Title = b.Title,
                CategoryId = b.CategoryId,
                AuthorIds = b.Authors.Select(a => a.Id).ToList()
            }).ToList();
        }
        public async Task CreateAsync(BookDto dto)
        {
            var category = await _categoryRepo.GetByIdAsync(dto.CategoryId);
            var authors = await _authorRepo.GetByIdsAsync(dto.AuthorIds);
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                CategoryId = dto.CategoryId,
                Category = category,
                Authors = authors
            };
            await _bookRepo.CreateAsync(book);
        }
        public async Task UpdateAsync(Guid id, BookDto dto)
        {
            if (dto == null) return;
            var category = await _categoryRepo.GetByIdAsync(dto.CategoryId);
            var authors = await _authorRepo.GetByIdsAsync(dto.AuthorIds);
            var book = new Book
            {
                Id = id,
                Title = dto.Title,
                CategoryId = dto.CategoryId,
                Category = category,
                Authors = authors
            };
            await _bookRepo.UpdateAsync(book);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _bookRepo.DeleteAsync(id);
        }
    }
}
