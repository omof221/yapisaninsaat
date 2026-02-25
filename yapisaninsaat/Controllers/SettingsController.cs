using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Helpers;
using yapisaninsaat.Models;

namespace yapisaninsaat.Controllers
{
    public class SettingsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SettingsController(AppDbContext context, IWebHostEnvironment env) { _context = context; _env = env; }

        public async Task<IActionResult> Index()
        {
            var setting = await _context.Settings.FirstOrDefaultAsync();
            if (setting == null)
            {
                setting = new Setting();
                _context.Settings.Add(setting);
                await _context.SaveChangesAsync();
            }
            return View(setting);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Setting item, IFormFile? LogoFile, IFormFile? FaviconFile, IFormFile? AboutImageFile)
        {
            if (ModelState.IsValid)
            {
                var existing = await _context.Settings.AsNoTracking().FirstOrDefaultAsync(s => s.Id == item.Id);

                if (LogoFile != null && LogoFile.Length > 0)
                {
                    FileHelper.DeleteImage(_env, existing?.LogoUrl);
                    var path = await FileHelper.UploadImageAsync(LogoFile, _env, "settings");
                    if (path != null) item.LogoUrl = path;
                }

                if (FaviconFile != null && FaviconFile.Length > 0)
                {
                    FileHelper.DeleteImage(_env, existing?.FaviconUrl);
                    var path = await FileHelper.UploadImageAsync(FaviconFile, _env, "settings");
                    if (path != null) item.FaviconUrl = path;
                }

                if (AboutImageFile != null && AboutImageFile.Length > 0)
                {
                    FileHelper.DeleteImage(_env, existing?.AboutImageUrl);
                    var path = await FileHelper.UploadImageAsync(AboutImageFile, _env, "settings");
                    if (path != null) item.AboutImageUrl = path;
                }

                _context.Update(item);
                await _context.SaveChangesAsync();
                ViewBag.Message = "Ayarlar başarıyla kaydedildi.";
            }
            return View(item);
        }
    }
}
