using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace BookStoreApp.Blazor.Server.UI.Services.Base
{
    public class BaseHttpService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorage;

        public BaseHttpService(IClient client, ILocalStorageService localStorage)
        {
            this._client = client;
            this._localStorage = localStorage;
        }

        protected Response<Guid> ConvertApiException<Guid>(ApiException apiException)
        {
            var response = new Response<Guid>()
            {
                Message = "Something went wrong, please try again.",
                Success = false
            };
            switch (apiException.StatusCode)
            {
                // Add more cases for each StatusCode

                case StatusCodes.Status201Created: 
                    response = new Response<Guid>()
                    {
                        Message = "Operation created successfully.",
                        Success = true
                    };
                    break;
                case StatusCodes.Status204NoContent:
                    response = new Response<Guid>()
                    {
                        Message = "Operation created successfully. No content returned.",
                        Success = true
                    };
                    break;
                case StatusCodes.Status400BadRequest:
                    response = new Response<Guid>()
                    {
                        Message = "Validation errors have occured.",
                        ValidationErrors = apiException.Response,
                        Success = false
                    };
                    break;
                case StatusCodes.Status401Unauthorized:
                    response = new Response<Guid>()
                    {
                        Message = "Invalid credentials. Please try again.",
                        ValidationErrors = apiException.Response,
                        Success = false
                    };
                    break;
                case StatusCodes.Status404NotFound:
                    response = new Response<Guid>()
                    {
                        Message = "The requested item could not be found.",
                        Success = false
                    };
                    break;
                case StatusCodes.Status500InternalServerError:
                    response = new Response<Guid>()
                    {
                        Message = "General internal server error.",
                        Success = false
                    };
                    break;
                default:
                    break;
            }

            return response;
        }

        protected async Task GetBearerToken()
        {
            var token = await _localStorage.GetItemAsync<string>("accessToken");
            if (token != null)
            {
                // Adding Bearer token to the Header
                _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
        }
    }
}
