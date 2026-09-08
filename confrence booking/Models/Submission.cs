namespace confrence_booking.Models
{
    public class Submission
    {
        public int Id { get; set; }

        // البيانات الشخصية
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // الكلية اختيار من القائمة المقابلة للـ Enum
        public CollegeType College { get; set; }
        public string Level { get; set; } = string.Empty;

        // تفاصيل البحث
        public string PaperTitle { get; set; } = string.Empty;
        public string AbstractText { get; set; } = string.Empty; // كتابة الـ Abstract كنص

        // مسار ملف البحث الكامل المرفوع
        public string FullPaperFilePath { get; set; } = string.Empty;

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }

    public enum CollegeType
    {
        Medicine = 1,                 // كلية الطب
        Dentistry = 2,                // كلية طب الفم والأسنان
        Pharmacy = 3,                 // كلية الصيدلة
        PhysicalTherapy = 4,          // كلية العلاج الطبيعي
        Nursing = 5,                  // كلية التمريض
        Engineering = 6,              // كلية الهندسة
        ComputersAndAI = 7,           // كلية الحاسبات والذكاء الاصطناعي
        Humanities = 8,               // كلية العلوم الإنسانية
        AppliedHealthSciences = 9     // كلية تكنولوجيا العلوم الصحية
    }
}