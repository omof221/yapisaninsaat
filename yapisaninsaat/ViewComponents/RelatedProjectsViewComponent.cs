using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class RelatedProjectsViewComponent : ViewComponent
    {
      private readonly AppDbContext _context;
        public RelatedProjectsViewComponent(AppDbContext context) => _context = context;

        public async Task<IViewComponentResult> InvokeAsync(int currentProjectId, int categoryId)
     {
            var projects = await _context.Projects
            .Where(p => p.IsActive && p.Id != currentProjectId)
       .Where(p => p.CategoryId == categoryId)
   .Include(p => p.Category)
         .Include(p => p.ProjectImages)
   .OrderByDescending(p => p.CreatedDate)
    .Take(3)
  .ToListAsync();

           // Yeterli proje yoksa diğer kategorilerden tamamla
    if (projects.Count < 3)
            {
   var moreProjects = await _context.Projects
          .Where(p => p.IsActive && p.Id != currentProjectId && p.CategoryId != categoryId)
           .Include(p => p.Category)
    .Include(p => p.ProjectImages)
   .OrderByDescending(p => p.CreatedDate)
   .Take(3 - projects.Count)
      .ToListAsync();
       projects.AddRange(moreProjects);
        }

            return View(projects);
        }
}
}
