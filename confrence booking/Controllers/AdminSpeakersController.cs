using confrence_booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace confrence_booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminSpeakersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminSpeakersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- SPEAKERS CRUD ---
        [HttpPost("speakers")]
        public async Task<IActionResult> CreateSpeaker([FromBody] Speaker speaker)
        {
            _context.Speakers.Add(speaker);
            await _context.SaveChangesAsync();
            return Ok(speaker);
        }

        [HttpPut("speakers/{id}")]
        public async Task<IActionResult> UpdateSpeaker(int id, [FromBody] Speaker speaker)
        {
            if (id != speaker.Id) return BadRequest("Id Mismatch");
            _context.Entry(speaker).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("speakers/{id}")]
        public async Task<IActionResult> DeleteSpeaker(int id)
        {
            var speaker = await _context.Speakers.FindAsync(id);
            if (speaker == null) return NotFound();
            _context.Speakers.Remove(speaker);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Speaker deleted successfully" });
        }

        // --- PARTNERS CRUD ---
        [HttpPost("partners")]
        public async Task<IActionResult> CreatePartner([FromBody] Partner partner)
        {
            _context.Partners.Add(partner);
            await _context.SaveChangesAsync();
            return Ok(partner);
        }

        [HttpDelete("partners/{id}")]
        public async Task<IActionResult> DeletePartner(int id)
        {
            var partner = await _context.Partners.FindAsync(id);
            if (partner == null) return NotFound();
            _context.Partners.Remove(partner);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Partner deleted successfully" });
        }
    }
}
