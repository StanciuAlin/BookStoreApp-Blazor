using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Providers;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IClient httpClient, 
            ILocalStorageService localStorage, 
            AuthenticationStateProvider authenticationStateProvider)
        {
            this._httpClient = httpClient;
            this._localStorage = localStorage;
            this._authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<bool> AuthenticateAsync(UserApiLoginDto loginDto)
        {
            var response = await _httpClient.LoginAsync(loginDto);

            // Store Token in local storage
            await _localStorage.SetItemAsync("accessToken", response.Token);

            // Change auth state of the app
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();

            return true;
        }

        public async Task Logout()
        {
            // Change auth state of the app
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
        }
    }
}
