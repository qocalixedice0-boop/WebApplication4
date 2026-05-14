using WebApplication4.Data;
using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly AppDbContext _context;
    
            public CategoryRepository(AppDbContext context)
            {
                _context = context;
            }
    
            public async Task<Category> GetByIdAsync(Guid id)
            {
                return await _context.Categories.FindAsync(id);
            }
    }
}
