using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Helpers;
using yapisaninsaat.Models;

namespace yapisaninsaat.Controllers
{
    public class ReferencesController : Controller
    {
       private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ReferencesController(AppDbContext context, IWebHostEnvironment env) { _context = context; _env = env; }

    public async Task<IActionResult> Index() => View(await _context.References.OrderBy(r => r.Order).ToListAsync());
      public IActionResult Create() => View();

 [HttpPost, ValidateAntiForgeryToken]
   public async Task<IActionResult> Create(Reference item, IFormFile? LogoFile)
{
   if (ModelState.IsValid)
     {
var path = await FileHelper.UploadImageAsync(LogoFile, _env, "references");
    if (path != null) item.LogoUrl = path;
  _context.Add(item);
 await _context.SaveChangesAsync();
     return RedirectToAction(nameof(Index));
     }
   return View(item);
        }

    public async Task<IActionResult> Edit(int? id)
        {
  if (id == null) return NotFound();
    var item = await _context.References.FindAsync(id);
  if (item == null) return NotFound();
     return View(item);
  }

     [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Reference item, IFormFile? LogoFile)
 {
if (id != item.Id) return NotFound();
       if (ModelState.IsValid)
   {
if (LogoFile != null && LogoFile.Length > 0)
     {
   var existing = await _context.References.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
     FileHelper.DeleteImage(_env, existing?.LogoUrl);
    var path = await FileHelper.UploadImageAsync(LogoFile, _env, "references");
    if (path != null) item.LogoUrl = path;
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
    var item = await _context.References.FindAsync(id);
    if (item == null) return NotFound();
    return View(item);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
   public async Task<IActionResult> DeleteConfirmed(int id)
     {
var item = await _context.References.FindAsync(id);
     if (item != null)
   {
    FileHelper.DeleteImage(_env, item.LogoUrl);
      _context.References.Remove(item);
    }
      await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
        }
    }
}
