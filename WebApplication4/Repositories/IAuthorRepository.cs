using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(Guid id);
        Task<List<Author>> GetByIdsAsync(List<Guid> ids);

        Task AddAsync(Author author);
        Task Update(Author author);
        Task Delete(Author author);
    }
}