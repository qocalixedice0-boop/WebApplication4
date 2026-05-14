using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetByIdAsync(Guid id);   
    }
}
