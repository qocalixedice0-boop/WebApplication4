using WebApplication4.DTOs;
using WebApplication4.Exceptions;
using WebApplication4.Models;
using WebApplication4.Repositories;

namespace WebApplication4.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepo;

        public AuthorService(IAuthorRepository authorRepo)
        {
            _authorRepo = authorRepo;
        }

        public async Task<List<AuthorResponseDto>> GetAuthorsAsync(bool includeBooks)
        {
            var authors = await _authorRepo.GetAllAsync(includeBooks);

            return authors.Select(a => new AuthorResponseDto
            {
                Id = a.Id,
                Name = a.Name,

                Books = includeBooks && a.Books != null
                    ? a.Books.Select(b => new BookDto
                    {
                        Id = b.Id,
                        Title = b.Title
                    }).ToList()
                    : new List<BookDto>()
            }).ToList();
        }

        public async Task<AuthorResponseDto> GetByIdAsync(Guid id, bool includeBooks)
        {
            var author = await _authorRepo.GetByIdAsync(id, includeBooks);

            if(author == null)
            {
                throw new NotFoundException("Author tapilmadi");
            }
          

            return new AuthorResponseDto
            {
                Id = author.Id,
                Name = author.Name,

                Books = includeBooks && author.Books != null
                    ? author.Books.Select(b => new BookDto
                    {
                        Id = b.Id,
                        Title = b.Title
                    }).ToList()
                    : new List<BookDto>()
            };
        }

        public async Task CreateAsync(CreateAuthorDto dto)
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };

            await _authorRepo.AddAsync(author);
        }

        public async Task UpdateAsync(Guid id, CreateAuthorDto dto)
        {
            var author = await _authorRepo.GetByIdAsync(id, false);

            if (author == null)
            {
                throw new NotFoundException("Author tapilmadi");
            }

            author.Name = dto.Name;

            await _authorRepo.UpdateAsync(author);
        }

        public async Task DeleteAsync(Guid id)
        {
            var author = await _authorRepo.GetByIdAsync(id, false);

            if (author == null)
            {
                throw new NotFoundException("Author tapilmadi");
            }

            await _authorRepo.DeleteAsync(author);
        }
    }
}