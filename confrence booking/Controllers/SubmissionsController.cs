using confrence_booking.DTOs;
using confrence_booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace confrence_booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public SubmissionsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SubmissionCreateDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("Please upload a valid file.");

            // إنشاء مجلد الحفظ إذا لم يكن موجوداً
            string uploadsFolder = Path.Combine(_environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // اسم الكود الفريد للملف لتفادي التكرار
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.File.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(fileStream);
            }

            var submission = new Submission
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                Email = dto.Email,
                College = dto.College,
                Level = dto.Level,
                Type = dto.Type,
                FilePath = "/uploads/" + uniqueFileName,
                SubmittedAt = DateTime.UtcNow
            };

            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registration & Submission successful!", submissionId = submission.Id });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var submissions = await _context.Submissions.ToListAsync();
            return Ok(submissions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var submission = await _context.Submissions.FindAsync(id);
            if (submission == null) return NotFound();
            return Ok(submission);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var submission = await _context.Submissions.FindAsync(id);
            if (submission == null) return NotFound();

            // حذف الملف المرفوق من المجلد أيضاً (اختياري)
            if (!string.IsNullOrEmpty(submission.FilePath))
            {
                var fullPath = Path.Combine(_environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), submission.FilePath.TrimStart('/'));
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }

            _context.Submissions.Remove(submission);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Submission deleted successfully" });
        }
    }
}
