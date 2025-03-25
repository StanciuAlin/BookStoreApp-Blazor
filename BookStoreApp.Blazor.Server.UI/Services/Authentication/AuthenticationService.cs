using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Providers;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly IClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IClient httpClient, 
            ILocalStorageService localStorage, 
            AuthenticationStateProvider authenticationStateProvider) : base(httpClient, localStorage)
        {
            this._httpClient = httpClient;
            this._localStorage = localStorage;
            this._authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<Response<AuthResponse>> AuthenticateAsync(UserApiLoginDto loginDto)
        {
            Response<AuthResponse> response;
            try
            {
                var result = await _httpClient.LoginAsync(loginDto);
                response = new Response<AuthResponse>
                {
                    Data = result,
                    Success = true
                };
                // Store Token in local storage
                await _localStorage.SetItemAsync("accessToken", result.Token);

                // Change auth state of the app
                await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();
            }
            catch (ApiException apiEx)
            {
                response = ConvertApiException<AuthResponse>(apiEx);
            }

            return response;
        }

        public async Task Logout()
        {
            // Change auth state of the app
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
        }
    }
}
