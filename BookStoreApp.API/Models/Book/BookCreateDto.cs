﻿using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Book
{
    public class BookCreateDto
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [Range(1200, int.MaxValue)]
        public int Year { get; set; }

        [Required]
        public string Isbn { get; set; } = null!;

        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string Summary { get; set; }

        public string? Image { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
    }
}
