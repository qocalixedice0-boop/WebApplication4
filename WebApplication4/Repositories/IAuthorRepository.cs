using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync(bool includeBooks);
        Task<Author> GetByIdAsync(Guid id, bool includeBooks);
        Task<List<Author>> GetByIdsAsync(List<Guid> ids);

        Task AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(Author author);
    }
}