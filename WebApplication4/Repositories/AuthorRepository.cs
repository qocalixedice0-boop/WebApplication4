using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public class AuthorRepository: IAuthorRepository
    {
        private readonly AppDbContext _context;
        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Author>> GetByIdsAsync(List<Guid> ids)
        {
            return await _context.Authors.Where(a => ids.Contains(a.Id)).ToListAsync();
        }
    }
}
