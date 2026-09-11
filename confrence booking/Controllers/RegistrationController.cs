using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using confrence_booking.DTOs;
using confrence_booking.Models;

namespace confrence_booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // تسجيل حضور جديد (Registration Only)
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateRegistrationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registration = new Registration
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                Email = dto.Email,
                RegisteredAt = DateTime.UtcNow
            };

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            return Ok(new { message = "تم تسجيل الحضور بنجاح", id = registration.Id });
        }

        // جلب قائمة الحاضرين للآدمن
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var registrations = await _context.Registrations
                .OrderByDescending(r => r.RegisteredAt)
                .ToListAsync();

            return Ok(registrations);
        }
    }
}