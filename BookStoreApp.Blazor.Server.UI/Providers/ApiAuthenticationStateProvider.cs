using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json.Bson;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStoreApp.Blazor.Server.UI.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        

        public ApiAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            this._localStorage = localStorage;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            return new AuthenticationState(user);
        }

        // Use LocalStorage in OnAfterRenderAsync, not in GetAuthenticationStateAsync
        public async Task<bool> LoggedIn()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            var savedToken = await _localStorage.GetItemAsync<string>("accessToken");
            if (!string.IsNullOrEmpty(savedToken))
            {
                var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
                // Check if the token is still valid
                if (tokenContent.ValidTo < DateTime.Now)
                {
                    // return new AuthenticationState(user);
                }
                var claims = tokenContent.Claims.ToList();
                claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
                user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
                var authState = Task.FromResult(new AuthenticationState(user));
                NotifyAuthenticationStateChanged(authState);
                return true;
            }
            else
            {
                LoggedOut();
                return false;
            }
        }

        public async Task LoggedOut()
        {
            await _localStorage.RemoveItemAsync("accessToken");
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);
        }

    }
}
