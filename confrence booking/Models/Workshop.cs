namespace confrence_booking.Models
{
    public class Workshop
    {
        public int Id { get; set; }
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string Instructor { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
