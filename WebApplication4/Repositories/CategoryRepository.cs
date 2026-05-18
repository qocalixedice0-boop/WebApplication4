using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<Category>> GetCategoryAsync(bool books)
        {
            var query = _context.Categories.AsQueryable();

            if (books)
            {
                query = query
                    .Include(c => c.Books)
                        .ThenInclude(b => b.Authors);
            }

            return await query.ToListAsync();
        }


        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _context.Categories
                .Include(c => c.Books)
                .ThenInclude(b => b.Authors)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Category category)
        {
            var category1 = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == category.Id);

            if (category1 == null)
                return;

            category1.Name = category.Name;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
                return;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
