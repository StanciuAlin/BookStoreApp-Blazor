using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
{
    public class AuthorService : BaseHttpService, IAuthorService
    {
        private readonly IClient _client;
        private readonly IMapper _mapper;

        public AuthorService(IClient client, 
            ILocalStorageService localStorage,
            IMapper mapper) : base(client, localStorage)
        {
            this._client = client;
            this._mapper = mapper;
        }

        public async Task<Response<int>> CreateAsync(AuthorCreateDto author)
        {
            Response<int> response = new Response<int>();

            try
            {
                await GetBearerToken();
                await _client.AuthorsPOSTAsync(author);
            }
            catch (ApiException apiEx)
            {
                response = ConvertApiException<int>(apiEx);
            }

            return response;
        }

        public async Task<Response<int>> UpdateAsync(int id, AuthorUpdateDto author)
        {
            Response<int> response = new Response<int>();

            try
            {
                await GetBearerToken();
                await _client.AuthorsPUTAsync(id, author);
            }
            catch (ApiException apiEx)
            {
                response = ConvertApiException<int>(apiEx);
            }

            return response;
        }

        public async Task<Response<List<AuthorReadOnlyDto>>> GetAllAsync()
        {
            Response<List<AuthorReadOnlyDto>> response;

            try
            {
                await GetBearerToken();
                var data = (await _client.AuthorsAllAsync()).ToList();
                response = new Response<List<AuthorReadOnlyDto>>()
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException apiEx)
            {
                response = ConvertApiException<List<AuthorReadOnlyDto>>(apiEx);
            }

            return response;
        }

        public async Task<Response<AuthorDetailsDto>> GetByIdAsync(int id)
        {
            Response<AuthorDetailsDto> response;

            try
            {
                await GetBearerToken();
                var data = (await _client.AuthorsGETAsync(id));
                response = new Response<AuthorDetailsDto>()
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException apiEx)
            {
                response = ConvertApiException<AuthorDetailsDto>(apiEx);
            }

            return response;
        }

        public async Task<Response<AuthorUpdateDto>> GetByIdForUpdateAsync(int id)
        {
            Response<AuthorUpdateDto> response;

            try
            {
                await GetBearerToken();
                var data = (await _client.AuthorsGETAsync(id));
                response = new Response<AuthorUpdateDto>()
                {
                    Data = _mapper.Map<AuthorUpdateDto>(data),
                    Success = true
                };
            }
            catch (ApiException apiEx)
            {
                response = ConvertApiException<AuthorUpdateDto>(apiEx);
            }

            return response;
        }

        public async Task<Response<int>> DeleteAsync(int id)
        {
            Response<int> response = new Response<int>();

            try
            {
                await GetBearerToken();
                await _client.AuthorsDELETEAsync(id);
            }
            catch (ApiException apiEx)
            {
                response = ConvertApiException<int>(apiEx);
            }

            return response;
        }
    }
}
