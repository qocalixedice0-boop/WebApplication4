using WebApplication4.Models;

namespace WebApplication4.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetByIdsAsync(List<Guid> ids);
    }
}
