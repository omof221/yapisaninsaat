using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Helpers;
using yapisaninsaat.Models;

namespace yapisaninsaat.Controllers
{
 public class ServicesController : Controller
    {
   private readonly AppDbContext _context;
      private readonly IWebHostEnvironment _env;
        public ServicesController(AppDbContext context, IWebHostEnvironment env) { _context = context; _env = env; }

        public async Task<IActionResult> Index() =>
  View(await _context.Services.OrderBy(s => s.Order).ToListAsync());

        public async Task<IActionResult> Details(int? id)
  {
            if (id == null) return NotFound();
            var service = await _context.Services.FindAsync(id);
   if (service == null) return NotFound();
 return View(service);
        }

     public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
   public async Task<IActionResult> Create(Service service, IFormFile? ImageFile)
        {
     if (ModelState.IsValid)
            {
var path = await FileHelper.UploadImageAsync(ImageFile, _env, "services");
if (path != null) service.ImageUrl = path;
         _context.Add(service);
    await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
            }
   return View(service);
        }

        public async Task<IActionResult> Edit(int? id)
   {
     if (id == null) return NotFound();
 var service = await _context.Services.FindAsync(id);
        if (service == null) return NotFound();
      return View(service);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Service service, IFormFile? ImageFile)
        {
            if (id != service.Id) return NotFound();
            if (ModelState.IsValid)
       {
     if (ImageFile != null && ImageFile.Length > 0)
{
        var existing = await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
 FileHelper.DeleteImage(_env, existing?.ImageUrl);
        var path = await FileHelper.UploadImageAsync(ImageFile, _env, "services");
        if (path != null) service.ImageUrl = path;
  }
        _context.Update(service);
       await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
          }
            return View(service);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
    var service = await _context.Services.FindAsync(id);
            if (service == null) return NotFound();
        return View(service);
        }

     [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
        {
      var service = await _context.Services.FindAsync(id);
   if (service != null)
          {
  FileHelper.DeleteImage(_env, service.ImageUrl);
 _context.Services.Remove(service);
        }
      await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
  }
    }
}
