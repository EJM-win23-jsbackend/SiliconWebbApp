using System.ComponentModel.DataAnnotations;

namespace BlazorWebbApp.Models
{
    public class SubscriberModel
    {
        public string? _email;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z]{2,}@[a-zA-Z]{2,}\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        public string Email 
        {
            get => _email ?? string.Empty;
            set => _email = value;
        }

        public bool IsChecked { get; set; } = false;
    }
}
