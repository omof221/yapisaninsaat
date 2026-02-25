using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class ProjectsSectionViewComponent : ViewComponent
    {
   private readonly AppDbContext _context;
   public ProjectsSectionViewComponent(AppDbContext context) => _context = context;

        public async Task<IViewComponentResult> InvokeAsync(int status, int take = 6)
        {
         var projects = await _context.Projects
      .Where(p => p.IsActive && p.ProjectStatus == status)
       .Include(p => p.Category)
    .Include(p => p.ProjectImages)
  .OrderByDescending(p => p.CreatedDate)
               .Take(take)
            .ToListAsync();

       ViewBag.Status = status;
  return View(projects);
    }
    }
}
