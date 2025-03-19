using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.User
{
    public class UserApiDto : UserApiLoginDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
