using Microsoft.AspNetCore.Mvc;
using WebApplication4.DTOs;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll( bool books = false)
        {
            var authors = await _authorService.GetAuthorsAsync(books);
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var author = await _authorService.GetByIdAsync(id);

            if (author == null)
                return NotFound("Author not found");

            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorDto dto)
        {
            await _authorService.CreateAsync(dto);
            return Ok("Author created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateAuthorDto dto)
        {
            await _authorService.UpdateAsync(id, dto);
            return Ok("Author updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _authorService.DeleteAsync(id);
            return Ok("Author deleted successfully");
        }
    }
}