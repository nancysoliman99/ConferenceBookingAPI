using confrence_booking.Models;
using Microsoft.AspNetCore.Http;

namespace confrence_booking.DTOs
{
    public class CreateSubmissionDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public CollegeType College { get; set; }
        public string Level { get; set; } = string.Empty;

        public string PaperTitle { get; set; } = string.Empty;
        public string AbstractText { get; set; } = string.Empty; // حقل نصي لكتابة الـ Abstract

        public IFormFile FullPaperFile { get; set; } = null!; // حقل رفع ملف البحث الكامل
    }
}