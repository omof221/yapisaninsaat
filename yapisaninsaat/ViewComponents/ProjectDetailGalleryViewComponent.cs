using Microsoft.AspNetCore.Mvc;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class ProjectDetailGalleryViewComponent : ViewComponent
    {
      public IViewComponentResult Invoke(Project project)
        {
  var images = project.ProjectImages?.Where(pi => !pi.IsCover).OrderBy(pi => pi.Order).ToList()
 ?? new List<ProjectImage>();
   return View(images);
        }
    }
}
