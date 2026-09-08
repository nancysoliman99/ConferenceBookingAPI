namespace confrence_booking.DTOs
{
    public class UpdateGalleryItemDto
    {
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; } // اختياري في حال عدم تغيير الصورة
    }
}
