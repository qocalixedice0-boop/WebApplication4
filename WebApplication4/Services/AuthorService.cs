using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.DTOs;
using WebApplication4.Models;

namespace WebApplication4.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(CreateAuthorDto dto)
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AuthorResponseDto>> GetAuthorsAsync(bool book)
        {
            return await _context.Authors
                .Select(a => new AuthorResponseDto
                {
                    Id = a.Id,
                    Name = a.Name   
                })
                .ToListAsync();
        }

        public async Task<AuthorResponseDto> GetByIdAsync(Guid id)
        {
            var author = await _context.Authors
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
                return null;

            return new AuthorResponseDto
            {
                Id = author.Id,
                Name = author.Name
            };
        }

        public async Task UpdateAsync(Guid id, CreateAuthorDto dto)
        {
            var author = await _context.Authors
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
                return;

            author.Name = dto.Name;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var author = await _context.Authors
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
                return;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}