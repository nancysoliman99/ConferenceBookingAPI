namespace confrence_booking.DTOs
{
    public class CreateGalleryItemDto
    {
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public IFormFile ImageFile { get; set; } = null!;
    }
}
