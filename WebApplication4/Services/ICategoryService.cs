using WebApplication4.DTOs;

namespace WebApplication4.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> GetCategoryAsync(bool includeBooks);
        Task<CategoryResponseDto> GetCategoryByIdAsync(Guid id);
        Task CreateAsync(CreateCategoryDto dto);
        Task UpdateAsync(Guid id, CreateCategoryDto dto);
        Task DeleteAsync(Guid id);
    }
}
