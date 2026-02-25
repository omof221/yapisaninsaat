using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Helpers;
using yapisaninsaat.Models;

namespace yapisaninsaat.Controllers
{
    public class PhotoExhibitionsController : Controller
    {
 private readonly AppDbContext _context;
 private readonly IWebHostEnvironment _env;
        public PhotoExhibitionsController(AppDbContext context, IWebHostEnvironment env) { _context = context; _env = env; }

        public async Task<IActionResult> Gallery()
        {
            // Tüm fotoğrafları sırasına göre getiriyoruz. 
            // Eğer sadece anasayfada seçilenleri istersen .Where(p => p.IsHomeActive) ekleyebilirsin.
            var photos = await _context.PhotoExhibitions
                .OrderBy(p => p.Order)
                .ToListAsync();

            return View(photos);
        }

        public async Task<IActionResult> Index() =>
          View(await _context.PhotoExhibitions.OrderBy(p => p.Order).ToListAsync());

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhotoExhibition item, IFormFile? ImageFile)
        {
            // ImageUrl alanı modelde [Required] ise, dosya henüz yüklenmediği için 
            // validasyondan geçemeyecektir. Bu satırla o hatayı temizliyoruz.
            ModelState.Remove("ImageUrl");

            if (ModelState.IsValid)
            {
                // Dosyayı yükle ve yolu al
                var path = await FileHelper.UploadImageAsync(ImageFile, _env, "exhibitions");

                if (path != null)
                {
                    item.ImageUrl = path; // Yüklenen yol modele atanıyor
                }

                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        public async Task<IActionResult> Edit(int? id)
        {
       if (id == null) return NotFound();
    var item = await _context.PhotoExhibitions.FindAsync(id);
     if (item == null) return NotFound();
        return View(item);
   }

        [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PhotoExhibition item, IFormFile? ImageFile)
        {
      if (id != item.Id) return NotFound();
            if (ModelState.IsValid)
  {
      if (ImageFile != null && ImageFile.Length > 0)
{
     var existing = await _context.PhotoExhibitions.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
     FileHelper.DeleteImage(_env, existing?.ImageUrl);
    var path = await FileHelper.UploadImageAsync(ImageFile, _env, "exhibitions");
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
      var item = await _context.PhotoExhibitions.FindAsync(id);
            if (item == null) return NotFound();
       return View(item);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
       var item = await _context.PhotoExhibitions.FindAsync(id);
          if (item != null)
        {
                FileHelper.DeleteImage(_env, item.ImageUrl);
              _context.PhotoExhibitions.Remove(item);
          }
      await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
