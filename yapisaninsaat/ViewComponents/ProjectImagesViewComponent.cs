using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class ProjectImagesViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ProjectImagesViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Anasayfada aktif olanları sırasına göre getirir
            var photos = await _context.PhotoExhibitions
                .Where(p => p.IsHomeActive)
                .OrderBy(p => p.Order)
                .ToListAsync();

            return View(photos);
        }
    }
}
