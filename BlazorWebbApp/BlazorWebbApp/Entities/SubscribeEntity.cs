﻿using System.ComponentModel.DataAnnotations;

namespace BlazorWebbApp.Entities
{
    public class SubscribeEntity
    {
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z]{2,}@[a-zA-Z]{2,}\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;

        public string? UserId { get; set; }
    }
}
