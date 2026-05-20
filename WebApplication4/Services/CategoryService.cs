using Microsoft.AspNetCore.Mvc;
using WebApplication4.DTOs;
using WebApplication4.Exceptions;
using WebApplication4.Models;
using WebApplication4.Repositories;

namespace WebApplication4.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }


        public async Task<List<CategoryResponseDto>> GetCategoryAsync(bool includeBooks)
        {
            var categories = await _categoryRepo.GetCategoryAsync(includeBooks);

            return categories.Select(c => new CategoryResponseDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

      

        public async Task<CategoryResponseDto> GetCategoryByIdAsync(Guid id, bool includeBooks)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(id, includeBooks);

            if (category == null)
            {
                throw new NotFoundException("Category tapilmadi");
            }

            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name,

                Books = includeBooks && category.Books != null
                    ? category.Books.Select(b => new BookDto
                    {
                        Id = b.Id,
                        Title = b.Title
                    }).ToList()
                    : new List<BookDto>()
            };
        }


        public async Task CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };

            await _categoryRepo.CreateAsync(category);
        }

        public async Task UpdateAsync(Guid id, CreateCategoryDto dto)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(id, false);

            if (category == null)
            {
                throw new NotFoundException("Category tapilmadi");
            }

            category.Name = dto.Name;

            await _categoryRepo.UpdateAsync(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(id,false);

            if (category == null)
            {
                throw new NotFoundException("Category tapilmadi");
            }

            await _categoryRepo.DeleteAsync(category);
        }
    }
}