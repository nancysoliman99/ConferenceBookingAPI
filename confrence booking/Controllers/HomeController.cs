using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace confrence_booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string ResolveLanguage(string lang)
        {
            if (!string.IsNullOrWhiteSpace(lang))
            {
                if (lang.ToLower().StartsWith("ar")) return "ar";
                if (lang.ToLower().StartsWith("en")) return "en";
            }

            var headerLang = Request.Headers["Accept-Language"].ToString().ToLower();
            if (headerLang.StartsWith("ar")) return "ar";

            return "ar"; // اللغة الافتراضية للسيستم هي العربية
        }

        [HttpGet("videos")]
        public async Task<IActionResult> GetInvitationVideos([FromQuery] string lang = "ar")
        {
            string currentLang = ResolveLanguage(lang);
            var videos = await _context.InvitationVideos
                .Select(v => new {
                    v.Id,
                    Title = currentLang == "ar" ? v.TitleAr : v.TitleEn,
                    v.VideoUrl
                }).ToListAsync();

            return Ok(videos);
        }

        [HttpGet("main-topics")]
        public async Task<IActionResult> GetMainTopics([FromQuery] string lang = "ar")
        {
            string currentLang = ResolveLanguage(lang);
            var topics = await _context.MainTopics
                .Select(t => new {
                    t.Id,
                    Title = currentLang == "ar" ? t.TitleAr : t.TitleEn,
                    Description = currentLang == "ar" ? t.DescriptionAr : t.DescriptionEn
                }).ToListAsync();

            return Ok(topics);
        }

        [HttpGet("workshops")]
        public async Task<IActionResult> GetWorkshops([FromQuery] string lang = "ar")
        {
            string currentLang = ResolveLanguage(lang);
            var workshops = await _context.Workshops
                .Select(w => new {
                    w.Id,
                    Title = currentLang == "ar" ? w.TitleAr : w.TitleEn,
                    w.Instructor,
                    w.Date
                }).ToListAsync();

            return Ok(workshops);
        }

        [HttpGet("speakers")]
        public async Task<IActionResult> GetSpeakers([FromQuery] string lang = "ar")
        {
            string currentLang = ResolveLanguage(lang);
            var speakers = await _context.Speakers
                .Select(s => new {
                    s.Id,
                    Name = currentLang == "ar" ? s.NameAr : s.NameEn,
                    Title = currentLang == "ar" ? s.TitleAr : s.TitleEn,
                    s.ImageUrl
                }).ToListAsync();

            return Ok(speakers);
        }

        [HttpGet("gallery")]
        public async Task<IActionResult> GetGallery([FromQuery] string lang = "ar")
        {
            string currentLang = ResolveLanguage(lang);
            var gallery = await _context.GalleryItems
                .Select(g => new {
                    g.Id,
                    Title = currentLang == "ar" ? g.TitleAr : g.TitleEn,
                    g.ImageUrl
                }).ToListAsync();

            return Ok(gallery);
        }
    }
}