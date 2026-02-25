using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Helpers;
using yapisaninsaat.Models;

namespace yapisaninsaat.Controllers
{
    public class ProjectImagesController : Controller
    {
   private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProjectImagesController(AppDbContext context, IWebHostEnvironment env) { _context = context; _env = env; }

        public async Task<IActionResult> Index() =>
  View(await _context.ProjectImages.Include(pi => pi.Project).OrderBy(pi => pi.ProjectId).ThenBy(pi => pi.Order).ToListAsync());

        // Proje bazlı görsel yönetim sayfası
public async Task<IActionResult> Manage(int? id)
        {
        if (id == null) return NotFound();
            var project = await _context.Projects
       .Include(p => p.ProjectImages.OrderBy(pi => pi.Order))
.FirstOrDefaultAsync(p => p.Id == id);
         if (project == null) return NotFound();
      return View(project);
      }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(int ProjectId, IFormFile? CoverFile, List<IFormFile>? AdditionalFiles)
        {
            var project = await _context.Projects.Include(p => p.ProjectImages).FirstOrDefaultAsync(p => p.Id == ProjectId);
          if (project == null) return NotFound();

     var existingCount = project.ProjectImages.Count;

  // Kapak fotoğrafı yükle
         if (CoverFile != null && CoverFile.Length > 0)
        {
 // Eski kapağı kaldır
var existingCover = project.ProjectImages.FirstOrDefault(pi => pi.IsCover);
   if (existingCover != null)
     {
         FileHelper.DeleteImage(_env, existingCover.ImageUrl);
                    _context.ProjectImages.Remove(existingCover);
  existingCount--;
}

         var coverPath = await FileHelper.UploadImageAsync(CoverFile, _env, "projects");
             if (coverPath != null)
{
      _context.ProjectImages.Add(new ProjectImage
         {
  ProjectId = ProjectId,
         ImageUrl = coverPath,
   IsCover = true,
      Order = 0
       });
  }
            }

        // Ek fotoğrafları yükle
      if (AdditionalFiles != null && AdditionalFiles.Count > 0)
 {
    var existingExtras = project.ProjectImages.Where(pi => !pi.IsCover).Count();
      var slotsLeft = 6 - existingExtras;

                if (slotsLeft > 0)
      {
int order = existingExtras + 1;
         foreach (var file in AdditionalFiles.Take(slotsLeft))
      {
         if (file.Length > 0)
 {
         var path = await FileHelper.UploadImageAsync(file, _env, "projects");
   if (path != null)
   {
  _context.ProjectImages.Add(new ProjectImage
       {
   ProjectId = ProjectId,
              ImageUrl = path,
       IsCover = false,
      Order = order++
        });
         }
       }
          }
      }
            }

            await _context.SaveChangesAsync();
            TempData["Message"] = "Görseller başarıyla yüklendi.";
   return RedirectToAction(nameof(Manage), new { id = ProjectId });
        }

        [HttpPost, ValidateAntiForgeryToken]
   public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.ProjectImages.FindAsync(id);
    if (image == null) return NotFound();

            var projectId = image.ProjectId;
       FileHelper.DeleteImage(_env, image.ImageUrl);
  _context.ProjectImages.Remove(image);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Görsel silindi.";
            return RedirectToAction(nameof(Manage), new { id = projectId });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SetCover(int id)
        {
            var image = await _context.ProjectImages.FindAsync(id);
if (image == null) return NotFound();

    // Aynı projedeki eski kapağı kaldır
         var oldCover = await _context.ProjectImages
          .Where(pi => pi.ProjectId == image.ProjectId && pi.IsCover)
              .ToListAsync();
 foreach (var oc in oldCover)
        {
                oc.IsCover = false;
 _context.Update(oc);
            }

       image.IsCover = true;
 _context.Update(image);
    await _context.SaveChangesAsync();

            TempData["Message"] = "Kapak fotoğrafı güncellendi.";
            return RedirectToAction(nameof(Manage), new { id = image.ProjectId });
        }

   // Eski Create/Edit/Delete sayfalarını da tutalım (Index'ten erişim için)
        public IActionResult Create()
        {
ViewBag.Projects = new SelectList(_context.Projects.OrderBy(p => p.Title), "Id", "Title");
            return View();
        }

   [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int ProjectId, IFormFile? CoverFile, List<IFormFile>? AdditionalFiles)
        {
    if (ProjectId <= 0)
   {
    ModelState.AddModelError("", "Lütfen bir proje seçin.");
 ViewBag.Projects = new SelectList(_context.Projects.OrderBy(p => p.Title), "Id", "Title", ProjectId);
   return View();
            }

  // Manage sayfasındaki Upload'a yönlendir
          return await Upload(ProjectId, CoverFile, AdditionalFiles) is RedirectToActionResult
     ? RedirectToAction(nameof(Manage), new { id = ProjectId })
    : RedirectToAction(nameof(Index));
        }

      public async Task<IActionResult> Edit(int? id)
        {
  if (id == null) return NotFound();
     var image = await _context.ProjectImages.FindAsync(id);
       if (image == null) return NotFound();
            ViewBag.Projects = new SelectList(_context.Projects.OrderBy(p => p.Title), "Id", "Title", image.ProjectId);
   return View(image);
   }

        [HttpPost, ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, ProjectImage image, IFormFile? ImageFile)
     {
            if (id != image.Id) return NotFound();
      if (ModelState.IsValid)
    {
           if (ImageFile != null && ImageFile.Length > 0)
            {
  var existing = await _context.ProjectImages.AsNoTracking().FirstOrDefaultAsync(pi => pi.Id == id);
         FileHelper.DeleteImage(_env, existing?.ImageUrl);
    var path = await FileHelper.UploadImageAsync(ImageFile, _env, "projects");
    if (path != null) image.ImageUrl = path;
           }
           _context.Update(image);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Manage), new { id = image.ProjectId });
         }
     ViewBag.Projects = new SelectList(_context.Projects.OrderBy(p => p.Title), "Id", "Title", image.ProjectId);
          return View(image);
      }

      public async Task<IActionResult> Delete(int? id)
  {
      if (id == null) return NotFound();
            var image = await _context.ProjectImages.Include(pi => pi.Project).FirstOrDefaultAsync(pi => pi.Id == id);
            if (image == null) return NotFound();
            return View(image);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
   {
         var image = await _context.ProjectImages.FindAsync(id);
    if (image != null)
     {
             var projectId = image.ProjectId;
      FileHelper.DeleteImage(_env, image.ImageUrl);
      _context.ProjectImages.Remove(image);
       await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Manage), new { id = projectId });
            }
   await _context.SaveChangesAsync();
       return RedirectToAction(nameof(Index));
        }
    }
}
