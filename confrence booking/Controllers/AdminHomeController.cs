using confrence_booking.DTOs;
using confrence_booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace confrence_booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminHomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminHomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- MAIN TOPICS ---
        [HttpPost("main-topics")]
        public async Task<IActionResult> CreateTopic([FromBody] MainTopic topic)
        {
            _context.MainTopics.Add(topic);
            await _context.SaveChangesAsync();
            return Ok(topic);
        }

        [HttpPut("main-topics/{id}")]
        public async Task<IActionResult> UpdateTopic(int id, [FromBody] MainTopic topic)
        {
            if (id != topic.Id) return BadRequest("Id Mismatch");
            _context.Entry(topic).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("main-topics/{id}")]
        public async Task<IActionResult> DeleteTopic(int id)
        {
            var topic = await _context.MainTopics.FindAsync(id);
            if (topic == null) return NotFound();
            _context.MainTopics.Remove(topic);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Topic deleted successfully" });
        }

        // --- WORKSHOPS ---
        [HttpPost("workshops")]
        public async Task<IActionResult> CreateWorkshop([FromBody] Workshop workshop)
        {
            _context.Workshops.Add(workshop);
            await _context.SaveChangesAsync();
            return Ok(workshop);
        }

        [HttpPut("workshops/{id}")]
        public async Task<IActionResult> UpdateWorkshop(int id, [FromBody] Workshop workshop)
        {
            if (id != workshop.Id) return BadRequest("Id Mismatch");
            _context.Entry(workshop).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("workshops/{id}")]
        public async Task<IActionResult> DeleteWorkshop(int id)
        {
            var workshop = await _context.Workshops.FindAsync(id);
            if (workshop == null) return NotFound();
            _context.Workshops.Remove(workshop);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Workshop deleted successfully" });
        }

        // --- INVITATION VIDEOS ---
        [HttpPost("videos")]
        public async Task<IActionResult> CreateVideo([FromBody] CreateInvitationVideoDto dto)
        {
            var video = new InvitationVideo
            {
                TitleAr = dto.TitleAr,
                TitleEn = dto.TitleEn,
                VideoUrl = dto.VideoUrl
            };

            _context.InvitationVideos.Add(video);
            await _context.SaveChangesAsync();

            return Ok(video);
        }

        [HttpDelete("videos/{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var video = await _context.InvitationVideos.FindAsync(id);
            if (video == null) return NotFound();
            _context.InvitationVideos.Remove(video);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Video deleted successfully" });
        }
    }
}
