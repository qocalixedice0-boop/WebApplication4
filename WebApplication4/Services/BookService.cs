using WebApplication4.DTOs;
using WebApplication4.Exceptions;
using WebApplication4.Models;
using WebApplication4.Repositories;

namespace WebApplication4.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IAuthorRepository _authorRepo;

        public BookService(
            IBookRepository bookRepo,
            ICategoryRepository categoryRepo,
            IAuthorRepository authorRepo)
        {
            _bookRepo = bookRepo;
            _categoryRepo = categoryRepo;
            _authorRepo = authorRepo;
        }

        public async Task<List<BookResponseDto>> GetBooksAsync(bool includeCategory, bool includeAuthors)
        {
            var books = await _bookRepo.GetBooksAsync(includeCategory, includeAuthors);

            return books.Select(b => new BookResponseDto
            {
                Id = b.Id,
                Title = b.Title,

                Category = includeCategory && b.Category != null
                    ? new CategoryDto
                    {
                        Id = b.Category.Id,
                        Name = b.Category.Name
                    }
                    : null,

                Authors = includeAuthors && b.Authors != null
                    ? b.Authors.Select(a => new AuthorDto
                    {
                        Id = a.Id,
                        Name = a.Name
                    }).ToList()
                    : new List<AuthorDto>()
            }).ToList();
        }


        public async Task<BookResponseDto> GetByIdAsync(Guid id, bool includeCategory, bool includeAuthors)
        {
            var book = await _bookRepo.GetByIdAsync(id, includeCategory, includeAuthors);
            if (book == null)
            {
                throw new NotFoundException("Book tapilmadi");
            }
            return new BookResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                Category = includeCategory && book.Category != null
                    ? new CategoryDto
                    {
                        Id = book.Category.Id,
                        Name = book.Category.Name
                    }
                    : null,
                Authors = includeAuthors && book.Authors != null
                    ? book.Authors.Select(a => new AuthorDto
                    {
                        Id = a.Id,
                        Name = a.Name
                    }).ToList()
                    : new List<AuthorDto>()
            };
        }

        public async Task CreateAsync(CreateBookDto dto)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(dto.CategoryId, false);

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
            var book = await _bookRepo.GetByIdAsync(id,false,false);

            if (book == null)
            {
                throw new NotFoundException("Book tapilmadi");
            }

            var category = await _categoryRepo.GetCategoryByIdAsync(dto.CategoryId, false);

            if(category == null)
            {
                throw new NotFoundException("Category tapilmadi");
            }

            var authors = await _authorRepo.GetByIdsAsync(dto.AuthorIds);

            if (authors == null || !authors.Any())
            {
                throw new NotFoundException("Authors tapilmadi");
            }

            book.Title = dto.Title;
            book.CategoryId = dto.CategoryId;
            book.Category = category;
            book.Authors = authors;

            await _bookRepo.UpdateAsync(book);
        }

        public async Task DeleteAsync(Guid id)
        {
            var book = await _bookRepo.GetByIdAsync(id, false, false);

            if (book == null)
            {
                throw new NotFoundException("Book tapilmadi");
            }

            await _bookRepo.DeleteAsync(book);
        }
    }
}