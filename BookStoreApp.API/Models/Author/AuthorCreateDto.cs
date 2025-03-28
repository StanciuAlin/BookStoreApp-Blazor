﻿using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Author
{
    public class AuthorCreateDto
    {
        // I don't want to show the Id
        //public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        [StringLength(250)]
        public string? Bio { get; set; } = string.Empty;
    }
}
