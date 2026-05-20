
using Microsoft.AspNetCore.Mvc;
using WebApplication4.DTOs;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;
        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] bool includeCategory, [FromQuery] bool includeAuthors)
        {
            var result = await _service.GetBooksAsync(includeCategory, includeAuthors);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id, [FromQuery] bool includeCategory, [FromQuery] bool includeAuthors)
        {
            var result = await _service.GetByIdAsync(id, includeCategory, includeAuthors);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto dto)
        {
            await _service.CreateAsync(dto);
            return Ok();
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CreateBookDto dto)
        {
            if(dto==null) return BadRequest();
           
            
            await _service.UpdateAsync(id,dto);
            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if(id==Guid.Empty) return BadRequest();
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
