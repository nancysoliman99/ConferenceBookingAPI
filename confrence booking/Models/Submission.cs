namespace confrence_booking.Models
{
    public class Submission
    {
        public int Id { get; set; }

        // البيانات الشخصية
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty; // Same for WhatsApp
        public string Email { get; set; } = string.Empty;
        public string College { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;

        // نوع التقديم والملف
        public SubmissionType Type { get; set; } // AbstractOnly or FullPaper
        public string FilePath { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }

    public enum SubmissionType
    {
        AbstractOnly = 1,
        FullPaper = 2
    }
}
