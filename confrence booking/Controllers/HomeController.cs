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
        protected string CurrentLanguage =>
        Request.Headers["Accept-Language"].ToString().ToLower().StartsWith("ar") ||
        Request.Query["lang"] == "ar" ? "ar" : "en";

        private string GetLang() =>
            Request.Headers["Accept-Language"].ToString().ToLower().StartsWith("ar") ? "ar" : "en";

        [HttpGet("videos")]
        public async Task<IActionResult> GetInvitationVideos()
        {
            string lang = GetLang();
            var videos = await _context.InvitationVideos
                .Select(v => new {
                    v.Id,
                    Title = lang == "ar" ? v.TitleAr : v.TitleEn,
                    v.VideoUrl
                }).ToListAsync();

            return Ok(videos);
        }

        [HttpGet("main-topics")]
        public async Task<IActionResult> GetMainTopics()
        {
            string lang = GetLang();
            var topics = await _context.MainTopics
                .Select(t => new {
                    t.Id,
                    Title = lang == "ar" ? t.TitleAr : t.TitleEn,
                    Description = lang == "ar" ? t.DescriptionAr : t.DescriptionEn
                }).ToListAsync();

            return Ok(topics);
        }

        [HttpGet("workshops")]
        public async Task<IActionResult> GetWorkshops()
        {
            string lang = GetLang();
            var workshops = await _context.Workshops
                .Select(w => new {
                    w.Id,
                    Title = lang == "ar" ? w.TitleAr : w.TitleEn,
                    w.Instructor,
                    w.Date
                }).ToListAsync();

            return Ok(workshops);
        }
    }
}
