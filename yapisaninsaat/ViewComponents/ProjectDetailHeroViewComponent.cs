using Microsoft.AspNetCore.Mvc;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class ProjectDetailHeroViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Project project)
        {
            return View(project);
        }
    }
}
