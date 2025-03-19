using Microsoft.AspNetCore.Identity;

namespace BookStoreApp.API.Data
{
    public class UserApi : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
