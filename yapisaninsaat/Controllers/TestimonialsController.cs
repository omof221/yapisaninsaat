using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Helpers;
using yapisaninsaat.Models;

namespace yapisaninsaat.Controllers
{
    public class TestimonialsController : Controller
  {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public TestimonialsController(AppDbContext context, IWebHostEnvironment env) { _context = context; _env = env; }

        public async Task<IActionResult> Index() => View(await _context.Testimonials.OrderBy(t => t.Order).ToListAsync());
  public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Testimonial item, IFormFile? ImageFile)
        {
     if (ModelState.IsValid)
 {
var path = await FileHelper.UploadImageAsync(ImageFile, _env, "testimonials");
      if (path != null) item.ImageUrl = path;
       _context.Add(item);
await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
         return View(item);
   }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
        var item = await _context.Testimonials.FindAsync(id);
       if (item == null) return NotFound();
   return View(item);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Testimonial item, IFormFile? ImageFile)
    {
            if (id != item.Id) return NotFound();
            if (ModelState.IsValid)
  {
             if (ImageFile != null && ImageFile.Length > 0)
        {
         var existing = await _context.Testimonials.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
FileHelper.DeleteImage(_env, existing?.ImageUrl);
               var path = await FileHelper.UploadImageAsync(ImageFile, _env, "testimonials");
      if (path != null) item.ImageUrl = path;
      }
         _context.Update(item);
        await _context.SaveChangesAsync();
       return RedirectToAction(nameof(Index));
      }
  return View(item);
        }

        public async Task<IActionResult> Delete(int? id)
        {
     if (id == null) return NotFound();
  var item = await _context.Testimonials.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
   {
    var item = await _context.Testimonials.FindAsync(id);
            if (item != null)
        {
  FileHelper.DeleteImage(_env, item.ImageUrl);
     _context.Testimonials.Remove(item);
       }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
}
}
