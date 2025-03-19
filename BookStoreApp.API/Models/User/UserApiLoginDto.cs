using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.User
{
    public class UserApiLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } // Email address of the user is the user name in this case

        [Required]
        public string Password { get; set; }
    }
}
