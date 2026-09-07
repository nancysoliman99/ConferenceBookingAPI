using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace confrence_booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConferenceDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ConferenceDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetLang() =>
            Request.Headers["Accept-Language"].ToString().ToLower().StartsWith("ar") ? "ar" : "en";

        [HttpGet("speakers")]
        public async Task<IActionResult> GetSpeakers()
        {
            string lang = GetLang();
            var speakers = await _context.Speakers
                .Select(s => new {
                    s.Id,
                    Name = lang == "ar" ? s.NameAr : s.NameEn,
                    Title = lang == "ar" ? s.TitleAr : s.TitleEn,
                    s.ImageUrl
                }).ToListAsync();

            return Ok(speakers);
        }

        [HttpGet("partners")]
        public async Task<IActionResult> GetPartners()
        {
            var partners = await _context.Partners.ToListAsync();
            return Ok(partners);
        }

        [HttpGet("gallery")]
        public async Task<IActionResult> GetGallery()
        {
            string lang = GetLang();
            var items = await _context.GalleryItems
                .Select(g => new {
                    g.Id,
                    Title = lang == "ar" ? g.TitleAr : g.TitleEn,
                    g.ImageUrl
                }).ToListAsync();

            return Ok(items);
        }
    }
}
