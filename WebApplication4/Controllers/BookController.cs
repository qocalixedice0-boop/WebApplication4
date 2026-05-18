using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using System.Threading.Tasks;
using WebApplication4.Data;
using WebApplication4.DTOs;
using WebApplication4.Models;
using WebApplication4.Repositories;
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
        public async Task<IActionResult> GetBooks( bool category=false,bool authors=false)
        {
            var result = await _service.GetBooksAsync(category,authors);
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
