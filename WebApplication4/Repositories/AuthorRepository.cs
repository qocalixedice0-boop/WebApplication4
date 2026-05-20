using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAsync(bool includeBooks)
        {
            var query = _context.Authors.AsQueryable();

            if (includeBooks)
            {
                query = query.Include(a => a.Books);
            }

            return await query.ToListAsync();
        }


        public async Task<Author> GetByIdAsync(Guid id, bool includeBooks = false)
        {
           var query = _context.Authors.AsQueryable();
            if (includeBooks)
            {
                query = query.Include(a => a.Books);
            }
            return await query.FirstOrDefaultAsync(a => a.Id == id);
        }


        public async Task<List<Author>> GetByIdsAsync(List<Guid> ids)
        {
            return await _context.Authors
                .Where(a => ids.Contains(a.Id))
                .ToListAsync();
        }

        
        public async Task AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        
        public async Task UpdateAsync(Author author)
        {
            var existing = await _context.Authors
                .FirstOrDefaultAsync(a => a.Id == author.Id);

            existing.Name = author.Name;

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Author author)
        { 
            _context.Authors.Remove(author);

            await _context.SaveChangesAsync();
        }
    }
}