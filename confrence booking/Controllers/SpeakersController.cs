using confrence_booking.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace confrence_booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SpeakersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // خاصية تحديد اللغة تلقائياً من الـ Header أو الـ Query
        private string CurrentLanguage =>
            Request.Headers["Accept-Language"].ToString().ToLower().StartsWith("ar") ||
            Request.Query["lang"] == "ar" ? "ar" : "en";

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var speakers = await _context.Speakers
                .Select(s => new SpeakerDto
                {
                    Id = s.Id,
                    Name = CurrentLanguage == "ar" ? s.NameAr : s.NameEn,
                    Title = CurrentLanguage == "ar" ? s.TitleAr : s.TitleEn,
                    ImageUrl = s.ImageUrl
                })
                .ToListAsync();

            return Ok(speakers);
        }

      
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var speaker = await _context.Speakers.FindAsync(id);
            if (speaker == null) return NotFound(new { message = "Speaker not found" });

            var speakerDto = new SpeakerDto
            {
                Id = speaker.Id,
                Name = CurrentLanguage == "ar" ? speaker.NameAr : speaker.NameEn,
                Title = CurrentLanguage == "ar" ? speaker.TitleAr : speaker.TitleEn,
                ImageUrl = speaker.ImageUrl
            };

            return Ok(speakerDto);
        }
    }
}
