using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
{
    public class BookService : BaseHttpService, IBookService
    {
        private readonly IClient _client;
        private readonly IMapper _mapper;

        public BookService(IClient client,
            ILocalStorageService localStorage,
            IMapper mapper) : base(client, localStorage)
        {
            this._client = client;
            this._mapper = mapper;
        }

        public async Task<Response<int>> CreateAsync(BookCreateDto book)
        {
            Response<int> response = new Response<int>();

            try
            {
                await GetBearerToken();
                await _client.BooksPOSTAsync(book);
            }
            catch (ApiException apiEx)
            {
                response = ConvertApiException<int>(apiEx);
            }

            return response;
        }

        public async Task<Response<int>> UpdateAsync(int id, BookUpdateDto book)
        {
            Response<int> response = new Response<int>();

            try
            {
                await GetBearerToken();
                await _client.BooksPUTAsync(id, book);
            }
            catch (ApiException apiEx)
            {
                response = ConvertApiException<int>(apiEx);
            }

            return response;
        }

        public async Task<Response<List<BookReadOnlyDto>>> GetAllAsync()
        {
            Response<List<BookReadOnlyDto>> response;

            try
            {
                await GetBearerToken();
                var data = (await _client.BooksAllAsync()).ToList();
                response = new Response<List<BookReadOnlyDto>>()
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException apiEx)
            {
                response = ConvertApiException<List<BookReadOnlyDto>>(apiEx);
            }

            return response;
        }

        public async Task<Response<BookDetailsDto>> GetByIdAsync(int id)
        {
            Response<BookDetailsDto> response;

            try
            {
                await GetBearerToken();
                var data = (await _client.BooksGETAsync(id));
                response = new Response<BookDetailsDto>()
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException apiEx)
            {
                response = ConvertApiException<BookDetailsDto>(apiEx);
            }

            return response;
        }

        public async Task<Response<BookUpdateDto>> GetByIdForUpdateAsync(int id)
        {
            Response<BookUpdateDto> response;

            try
            {
                await GetBearerToken();
                var data = (await _client.BooksGETAsync(id));
                response = new Response<BookUpdateDto>()
                {
                    Data = _mapper.Map<BookUpdateDto>(data),
                    Success = true
                };
            }
            catch (ApiException apiEx)
            {
                response = ConvertApiException<BookUpdateDto>(apiEx);
            }

            return response;
        }

        public async Task<Response<int>> DeleteAsync(int id)
        {
            Response<int> response = new Response<int>();

            try
            {
                await GetBearerToken();
                await _client.BooksDELETEAsync(id);
            }
            catch (ApiException apiEx)
            {
                response = ConvertApiException<int>(apiEx);
            }

            return response;
        }
    }
}
