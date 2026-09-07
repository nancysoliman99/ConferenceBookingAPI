using confrence_booking.Models;
using System.ComponentModel.DataAnnotations;

namespace confrence_booking.DTOs
{
    public class SubmissionCreateDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required, Phone]
        public string Phone { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string College { get; set; } = string.Empty;

        [Required]
        public string Level { get; set; } = string.Empty;

        [Required]
        public SubmissionType Type { get; set; }

        [Required]
        public IFormFile File { get; set; } = default!;
    }
}
