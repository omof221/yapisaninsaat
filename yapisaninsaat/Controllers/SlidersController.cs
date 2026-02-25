using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Helpers;
using yapisaninsaat.Models;

namespace yapisaninsaat.Controllers
{
    public class SlidersController : Controller
    {
 private readonly AppDbContext _context;
   private readonly IWebHostEnvironment _env;
     public SlidersController(AppDbContext context, IWebHostEnvironment env) { _context = context; _env = env; }

        public async Task<IActionResult> Index() => View(await _context.Sliders.OrderBy(s => s.Order).ToListAsync());

      public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
     public async Task<IActionResult> Create(Slider item, IFormFile? ImageFile)
      {
 if (ModelState.IsValid)
   {
 var path = await FileHelper.UploadImageAsync(ImageFile, _env, "sliders");
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
     var item = await _context.Sliders.FindAsync(id);
       if (item == null) return NotFound();
     return View(item);
        }

   [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Slider item, IFormFile? ImageFile)
  {
  if (id != item.Id) return NotFound();
     if (ModelState.IsValid)
  {
    if (ImageFile != null && ImageFile.Length > 0)
      {
 var existing = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
    FileHelper.DeleteImage(_env, existing?.ImageUrl);
      var path = await FileHelper.UploadImageAsync(ImageFile, _env, "sliders");
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
            var item = await _context.Sliders.FindAsync(id);
     if (item == null) return NotFound();
         return View(item);
   }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
   public async Task<IActionResult> DeleteConfirmed(int id)
        {
  var item = await _context.Sliders.FindAsync(id);
         if (item != null)
  {
FileHelper.DeleteImage(_env, item.ImageUrl);
     _context.Sliders.Remove(item);
     }
  await _context.SaveChangesAsync();
   return RedirectToAction(nameof(Index));
        }
    }
}
