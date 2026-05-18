using WebApplication4.DTOs;

namespace WebApplication4.Services
{
    public interface IAuthorService
    {
        Task<List<AuthorResponseDto>> GetAuthorsAsync(bool include);
        Task<AuthorResponseDto> GetByIdAsync(Guid id);
        Task CreateAsync(CreateAuthorDto dto);
        Task UpdateAsync(Guid id,CreateAuthorDto dto);
        Task DeleteAsync(Guid id);
    }
}
