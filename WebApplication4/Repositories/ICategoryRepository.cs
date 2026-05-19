using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryByIdAsync(Guid id);   
       Task<List<Category>> GetCategoryAsync(bool includeBooks);
       Task CreateAsync(Category category);
       Task UpdateAsync(Category category);
       Task DeleteAsync(Guid id);
    }
}
