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


        public async Task<List<Category>> GetCategoryAsync(bool includeBooks)
        {
            var query = _context.Categories.AsQueryable();

            if (includeBooks)
            {
                query = query
                    .Include(c => c.Books);
                        
            }

            return await query.ToListAsync();
        }

        


        public async Task<Category> GetCategoryByIdAsync(Guid id, bool includeBooks = false)
        {
            var query = _context.Categories.AsQueryable();

            if (includeBooks)
            {
                query = query
                    .Include(c => c.Books);
            }

            return await query.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Category category)
        {
            var existingCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == category.Id);

            existingCategory.Name = category.Name;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
