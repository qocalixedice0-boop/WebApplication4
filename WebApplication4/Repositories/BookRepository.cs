using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication4.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Book>> GetBooksAsync(
     bool includeCategory,
     bool includeAuthors)
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


        public async Task<Book> GetByIdAsync(Guid id)
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.Id == id);
        }


        public async Task CreateAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Book book)
        {
            var book1 = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == book.Id);

            if (book1 == null)
                return;

            book1.Title = book.Title;
            book1.CategoryId = book.CategoryId;

            var authors = await _context.Authors
                .Where(a => book.Authors.Select(x => x.Id).Contains(a.Id))
                .ToListAsync();

            book1.Authors = authors;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var book1 = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book1 == null)
            {
                return;
            }
            _context.Books.Remove(book1);
            await _context.SaveChangesAsync();
        }
    }
}
