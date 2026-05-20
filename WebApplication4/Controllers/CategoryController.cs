using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.DTOs;
using WebApplication4.Models;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool includeBooks = false)
        {
            var categories = await _categoryService.GetCategoryAsync(includeBooks);

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, [FromQuery] bool includeBooks = false)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id, includeBooks);

            if (category == null)
                return NotFound();

            return Ok(category);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            await _categoryService.CreateAsync(dto);
            return Ok();
        }

        
       

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CreateCategoryDto dto)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id, false);

            if (category == null)
                return NotFound();

            await _categoryService.UpdateAsync(id, dto);
            return NoContent();
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id, false);

            if (category == null)
                return NotFound();

            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
