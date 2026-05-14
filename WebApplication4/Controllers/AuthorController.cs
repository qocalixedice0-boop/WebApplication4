using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.DTOs;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthorsController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(AuthorDto dto)
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return Ok(author);
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Authors.ToListAsync());
        }

 

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, AuthorDto dto)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();

            author.Name = dto.Name;
            await _context.SaveChangesAsync();

            return Ok(author);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
