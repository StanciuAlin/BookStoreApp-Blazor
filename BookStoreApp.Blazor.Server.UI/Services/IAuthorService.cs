using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorReadOnlyDto>>> GetAllAsync();
        Task<Response<AuthorDetailsDto>> GetByIdAsync(int id);
        Task<Response<AuthorUpdateDto>> GetByIdForUpdateAsync(int id);
        
        // I am not expecting data because I do not need data after the creation operation
        Task<Response<int>> CreateAsync(AuthorCreateDto author);

        Task<Response<int>> UpdateAsync(int id, AuthorUpdateDto author);
        Task<Response<int>> DeleteAsync(int id);
    }
}