using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
{
    public interface IBookService
    {
        Task<Response<List<BookReadOnlyDto>>> GetAllAsync();
        Task<Response<BookDetailsDto>> GetByIdAsync(int id);
        Task<Response<BookUpdateDto>> GetByIdForUpdateAsync(int id);

        // I am not expecting data because I do not need data after the creation operation
        Task<Response<int>> CreateAsync(BookCreateDto book);

        Task<Response<int>> UpdateAsync(int id, BookUpdateDto book);
        Task<Response<int>> DeleteAsync(int id);
    }
}