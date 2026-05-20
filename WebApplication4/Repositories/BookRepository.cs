using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.DTOs;
using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Book>> GetBooksAsync(bool includeCategory, bool includeAuthors)
        {
            IQueryable<Book> query = _context.Books;

            if (includeCategory)
            {
                query = query.Include(b => b.Category);
            }

            if (includeAuthors)
            {
                query = query.Include(b => b.Authors);
            }

           

            return await query.ToListAsync();
        }


        public async Task<Book> GetByIdAsync(Guid id, bool includeCategory,bool includeAuthors)
        {
            IQueryable<Book> q = _context.Books;

            if (includeCategory)
                q = q.Include(x => x.Category);

            if (includeAuthors)
                q = q.Include(x => x.Authors);

            return await q.FirstOrDefaultAsync(x => x.Id == id);
        }

        


        public async Task CreateAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Book book)
        {
            var existingBook = await _context.Books
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.Id == book.Id);

            existingBook.Title = book.Title;
            existingBook.CategoryId = book.CategoryId;

            var authors = await _context.Authors
                .Where(a => book.Authors.Select(x => x.Id).Contains(a.Id))
                .ToListAsync();

            existingBook.Authors = authors;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
