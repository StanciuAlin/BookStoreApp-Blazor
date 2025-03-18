using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Author
{
    // Define this model for Read operation
    public class AuthorReadOnlyDto : BaseDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
    }
}
