using Microsoft.AspNetCore.Mvc;
using WebApplication4.DTOs;
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

        
        public async Task<List<CategoryResponseDto>> GetCategoryAsync(bool books)
        {
            var categories = await _categoryRepo.GetCategoryAsync(books);

            return categories.Select(c => new CategoryResponseDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public async Task<CategoryResponseDto> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(id);

            if (category == null) return null;

            return new CategoryResponseDto
            {
                Id = category.Id,
                Name = category.Name
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
            var category = await _categoryRepo.GetCategoryByIdAsync(id);

            if (category == null) return;

            category.Name = dto.Name;

            await _categoryRepo.UpdateAsync(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _categoryRepo.GetCategoryByIdAsync(id);

            if (category == null) return;

            await _categoryRepo.DeleteAsync(id);
        }
    }
}