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
        public async Task<List<BookResponseDto>> GetBooksAsync(bool include)
        {
            var books = await _bookRepo.GetBooksAsync(include);
            return books.Select(b => new BookResponseDto
            {
                Id = b.Id,
                Title = b.Title,
                CategoryName = b.Category != null ? b.Category.Name : null,
                Authors = b.Authors != null
                ? b.Authors.Select(a => new AuthorDto
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToList()
                : new List<AuthorDto>()
            }).ToList();
        }
        public async Task CreateAsync(CreateBookDto dto)
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
        public async Task UpdateAsync(Guid id, CreateBookDto dto)
        {
            var book = await _bookRepo.GetByIdAsync(id);

            if (book == null)
                return;

            var category = await _categoryRepo.GetByIdAsync(dto.CategoryId);
            var authors = await _authorRepo.GetByIdsAsync(dto.AuthorIds);

            
            book.Title = dto.Title;
            book.CategoryId = dto.CategoryId;
            book.Category = category;
            book.Authors = authors;

            await _bookRepo.UpdateAsync(book);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _bookRepo.DeleteAsync(id);
        }
    }
}
