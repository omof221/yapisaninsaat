using Microsoft.AspNetCore.Mvc;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class ProjectDetailBodyViewComponent : ViewComponent
    {
  private readonly AppDbContext _context;
        public ProjectDetailBodyViewComponent(AppDbContext context) => _context = context;

 public IViewComponentResult Invoke(Project project)
        {
 return View(project);
     }
    }
}
