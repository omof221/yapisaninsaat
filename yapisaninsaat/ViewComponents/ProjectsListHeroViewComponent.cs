using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class ProjectsListHeroViewComponent : ViewComponent
    {
  private readonly AppDbContext _context;
        public ProjectsListHeroViewComponent(AppDbContext context) => _context = context;

        public async Task<IViewComponentResult> InvokeAsync(int? durum)
        {
 var coverProject = await _context.Projects
       .Where(p => p.IsActive && p.IsFeatured)
          .Include(p => p.ProjectImages)
   .OrderByDescending(p => p.CreatedDate)
    .FirstOrDefaultAsync();

   ViewBag.Durum = durum;
            ViewBag.CoverImage = coverProject?.ProjectImages?
     .FirstOrDefault(pi => pi.IsCover)?.ImageUrl
    ?? coverProject?.ProjectImages?.FirstOrDefault()?.ImageUrl;

        return View();
        }
    }
}
