using confrence_booking.DTOs;
using confrence_booking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class GalleryController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public GalleryController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    private string CurrentLanguage =>
        Request.Headers["Accept-Language"].ToString().ToLower().StartsWith("ar") ||
        Request.Query["lang"] == "ar" ? "ar" : "en";

    // 1. READ ALL (جلب كل الصور باللغة المحددة)
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? lang)
    {
        var selectedLang = !string.IsNullOrEmpty(lang) ? lang : CurrentLanguage;

        var items = await _context.GalleryItems
            .Select(g => new GalleryItemDto
            {
                Id = g.Id,
                Title = selectedLang == "ar" ? g.TitleAr : g.TitleEn,
                ImageUrl = g.ImageUrl
            })
            .ToListAsync();

        return Ok(items);
    }

    // 2. READ BY ID (جلب صورة واحدة)
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, [FromQuery] string? lang)
    {
        var item = await _context.GalleryItems.FindAsync(id);
        if (item == null) return NotFound(new { message = "الصورة غير موجودة" });

        var selectedLang = !string.IsNullOrEmpty(lang) ? lang : CurrentLanguage;

        var dto = new GalleryItemDto
        {
            Id = item.Id,
            Title = selectedLang == "ar" ? item.TitleAr : item.TitleEn,
            ImageUrl = item.ImageUrl
        };

        return Ok(dto);
    }

    // 3. CREATE (إضافة صورة جديدة)
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateGalleryItemDto dto)
    {
        if (dto.ImageFile == null || dto.ImageFile.Length == 0)
            return BadRequest(new { message = "يرجى رفع صورة صالحة" });

        // حفظ الصورة في wwwroot/uploads/gallery
        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "gallery");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.ImageFile.CopyToAsync(stream);
        }

        var galleryItem = new GalleryItem
        {
            TitleAr = dto.TitleAr,
            TitleEn = dto.TitleEn,
            ImageUrl = $"/uploads/gallery/{uniqueFileName}"
        };

        _context.GalleryItems.Add(galleryItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = galleryItem.Id }, galleryItem);
    }

    // 4. UPDATE (تعديل صورة أو عنوان)
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateGalleryItemDto dto)
    {
        var item = await _context.GalleryItems.FindAsync(id);
        if (item == null) return NotFound(new { message = "الصورة غير موجودة" });

        item.TitleAr = dto.TitleAr;
        item.TitleEn = dto.TitleEn;

        // إذا تم رفع صورة جديدة يتم استبدال القديمة
        if (dto.ImageFile != null && dto.ImageFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "gallery");
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.ImageFile.CopyToAsync(stream);
            }

            item.ImageUrl = $"/uploads/gallery/{uniqueFileName}";
        }

        _context.GalleryItems.Update(item);
        await _context.SaveChangesAsync();

        return Ok(new { message = "تم التعديل بنجاح", item });
    }

    // 5. DELETE (حذف صورة)
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.GalleryItems.FindAsync(id);
        if (item == null) return NotFound(new { message = "الصورة غير موجودة" });

        _context.GalleryItems.Remove(item);
        await _context.SaveChangesAsync();

        return Ok(new { message = "تم الحذف بنجاح" });
    }
}