using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class HeroSliderViewComponent : ViewComponent
    {
    private readonly AppDbContext _context;
  public HeroSliderViewComponent(AppDbContext context) => _context = context;

        public async Task<IViewComponentResult> InvokeAsync()
        {
   var sliders = await _context.Sliders
              .Where(s => s.IsActive)
    .OrderBy(s => s.Order)
  .ToListAsync();

 var featuredProjects = await _context.Projects
   .Where(p => p.IsActive && p.IsFeatured)
        .Include(p => p.Category)
        .Include(p => p.ProjectImages)
       .OrderByDescending(p => p.CreatedDate)
       .Take(6)
            .ToListAsync();

     ViewBag.Sliders = sliders;
            return View(featuredProjects);
 }
    }
}
