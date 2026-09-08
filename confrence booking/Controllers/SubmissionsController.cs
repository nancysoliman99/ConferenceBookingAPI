using ClosedXML.Excel;
using confrence_booking.DTOs;
using confrence_booking.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.InkML;
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

        // 1. Endpoint لإرجاع قائمة الكليات المتاحة للفرونت إند (Dropdown)
        [HttpGet("colleges")]
        public IActionResult GetColleges([FromQuery] string? lang)
        {
            var selectedLang = !string.IsNullOrEmpty(lang) ? lang.ToLower() : "ar";

            var colleges = new[]
            {
                new { Id = (int)CollegeType.Medicine, Name = selectedLang == "ar" ? "كلية الطب" : "Faculty of Medicine" },
                new { Id = (int)CollegeType.Dentistry, Name = selectedLang == "ar" ? "كلية طب الفم والأسنان" : "Faculty of Dentistry" },
                new { Id = (int)CollegeType.Pharmacy, Name = selectedLang == "ar" ? "كلية الصيدلة" : "Faculty of Pharmacy" },
                new { Id = (int)CollegeType.PhysicalTherapy, Name = selectedLang == "ar" ? "كلية العلاج الطبيعي" : "Faculty of Physical Therapy" },
                new { Id = (int)CollegeType.Nursing, Name = selectedLang == "ar" ? "كلية التمريض" : "Faculty of Nursing" },
                new { Id = (int)CollegeType.Engineering, Name = selectedLang == "ar" ? "كلية الهندسة" : "Faculty of Engineering" },
                new { Id = (int)CollegeType.ComputersAndAI, Name = selectedLang == "ar" ? "كلية الحاسبات والذكاء الاصطناعي" : "Faculty of Computers & AI" },
                new { Id = (int)CollegeType.Humanities, Name = selectedLang == "ar" ? "كلية العلوم الإنسانية" : "Faculty of Humanities" },
                new { Id = (int)CollegeType.AppliedHealthSciences, Name = selectedLang == "ar" ? "كلية تكنولوجيا العلوم الصحية" : "Faculty of Applied Health Sciences" }
            };

            return Ok(colleges);
        }
       

[HttpGet("export-excel")]
    public async Task<IActionResult> ExportToExcel()
    {
        var submissions = await _context.Submissions.ToListAsync();

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Submissions");

            // 1. إضافة عناوين الأعمدة (Headers)
            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "الاسم الأول";
            worksheet.Cell(1, 3).Value = "الاسم الثاني";
            worksheet.Cell(1, 4).Value = "رقم الهاتف";
            worksheet.Cell(1, 5).Value = "البريد الإلكتروني";
            worksheet.Cell(1, 6).Value = "الكلية";
            worksheet.Cell(1, 7).Value = "المستوى الأكاديمي";
            worksheet.Cell(1, 8).Value = "عنوان البحث";
            worksheet.Cell(1, 9).Value = "ملخص البحث";
            worksheet.Cell(1, 10).Value = "رابط الملف";
            worksheet.Cell(1, 11).Value = "تاريخ التقديم";

            // تنسيق الهيدر (تغميق الخط وتغيير الخلفية)
            var headerRow = worksheet.Row(1);
            headerRow.Style.Font.Bold = true;
            headerRow.Style.Fill.BackgroundColor = XLColor.FromHtml("#1F4E78");
            headerRow.Style.Font.FontColor = XLColor.White;

            // 2. تعبئة البيانات (Data Rows)
            int row = 2;
            foreach (var item in submissions)
            {
                worksheet.Cell(row, 1).Value = item.Id;
                worksheet.Cell(row, 2).Value = item.FirstName;
                worksheet.Cell(row, 3).Value = item.LastName;
                worksheet.Cell(row, 4).Value = item.Phone;
                worksheet.Cell(row, 5).Value = item.Email;
                worksheet.Cell(row, 6).Value = item.College.ToString();
                worksheet.Cell(row, 7).Value = item.Level;
                worksheet.Cell(row, 8).Value = item.PaperTitle;
                worksheet.Cell(row, 9).Value = item.AbstractText;
                worksheet.Cell(row, 10).Value = item.FullPaperFilePath;
                worksheet.Cell(row, 11).Value = item.SubmittedAt.ToString("yyyy-MM-dd HH:mm");
                row++;
            }

            // ضبط عرض الأعمدة تلقائياً ليناسب المحتوى
            worksheet.Columns().AdjustToContents();

            // 3. تحويل الملف إلى MemoryStream وإرجاعه للعميل
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                string fileName = $"Submissions_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                return File(
                    content,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName
                );
            }
        }
    }
    // 2. رفع البحث والبيانات
    [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateSubmissionDto dto)
        {
            if (dto.FullPaperFile == null || dto.FullPaperFile.Length == 0)
                return BadRequest(new { message = "Please upload a valid full paper file." });

            // إنشاء مجلد الحفظ إذا لم يكن موجوداً
            string uploadsFolder = Path.Combine(_environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "uploads", "submissions");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // اسم ملف فريد
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.FullPaperFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await dto.FullPaperFile.CopyToAsync(fileStream);
            }

            var submission = new Submission
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                Email = dto.Email,
                College = dto.College,
                Level = dto.Level,
                PaperTitle = dto.PaperTitle,
                AbstractText = dto.AbstractText,
                FullPaperFilePath = "/uploads/submissions/" + uniqueFileName,
                SubmittedAt = DateTime.UtcNow
            };

            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registration & Submission successful!", submissionId = submission.Id });
        }

        // 3. جلب كافة الأبحاث
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var submissions = await _context.Submissions.ToListAsync();
            return Ok(submissions);
        }

        // 4. جلب بحث بواسطة الرقم المرجعي
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var submission = await _context.Submissions.FindAsync(id);
            if (submission == null) return NotFound(new { message = "Submission not found" });
            return Ok(submission);
        }

        // 5. حذف بحث مع الملف الخاص به
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var submission = await _context.Submissions.FindAsync(id);
            if (submission == null) return NotFound(new { message = "Submission not found" });

            if (!string.IsNullOrEmpty(submission.FullPaperFilePath))
            {
                var fullPath = Path.Combine(_environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), submission.FullPaperFilePath.TrimStart('/'));
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