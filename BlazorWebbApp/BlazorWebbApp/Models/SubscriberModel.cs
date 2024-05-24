using System.ComponentModel.DataAnnotations;

namespace BlazorWebbApp.Models
{
    public class SubscriberModel
    {
        public bool IsChecked { get; set; } = false;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z]{2,}@[a-zA-Z]{2,}\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;
    }
}
