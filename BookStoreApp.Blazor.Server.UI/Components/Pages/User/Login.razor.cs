using BookStoreApp.Blazor.Server.UI.Services.Authentication;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components;

namespace BookStoreApp.Blazor.Server.UI.Components.Pages.User
{
    public partial class Login : ComponentBase
    {
        [Inject] IAuthenticationService _authService { get; set; }
        [Inject] NavigationManager _navManager { get; set; }

        [SupplyParameterFromForm]
        private UserApiLoginDto? LoginModel { get; set; }
        private string _message = string.Empty;

        protected override void OnInitialized() => LoginModel ??= new();

        private async Task HandleLogin()
        {
            var response = await _authService.AuthenticateAsync(LoginModel);

            if (response.Success)
            {
                NavigateToHome();
            }

            //_message = "Invalid credentials. Please try again.";
            _message = response.Message;
        }

        private void NavigateToHome()
        {
            _navManager.NavigateTo("/");

        }

        // Make sure that JS calls are done only after the first render
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                StateHasChanged();
            }
        }
    }
}
