using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly AppDbContext _context;
        public ProjectsController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index() =>
          View(await _context.Projects.Include(p => p.Category).OrderByDescending(p => p.CreatedDate).ToListAsync());

        public async Task<IActionResult> Details(int? id)
        {
      if (id == null) return NotFound();
            var project = await _context.Projects.Include(p => p.Category).Include(p => p.ProjectImages.OrderBy(i => i.Order))
  .FirstOrDefaultAsync(p => p.Id == id);
            if (project == null) return NotFound();
            return View(project);
        }

        public IActionResult Create()
   {
            ViewBag.Categories = new SelectList(_context.ProjectCategories.Where(c => c.IsActive).OrderBy(c => c.Order), "Id", "Title");
        return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(Project project)
     {
         if (ModelState.IsValid)
            {
        project.CreatedDate = DateTime.Now;
        _context.Add(project);
        await _context.SaveChangesAsync();
   return RedirectToAction(nameof(Index));
   }
        ViewBag.Categories = new SelectList(_context.ProjectCategories.Where(c => c.IsActive).OrderBy(c => c.Order), "Id", "Title", project.CategoryId);
 return View(project);
    }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
     var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();
     ViewBag.Categories = new SelectList(_context.ProjectCategories.Where(c => c.IsActive).OrderBy(c => c.Order), "Id", "Title", project.CategoryId);
       return View(project);
 }

        [HttpPost, ValidateAntiForgeryToken]
 public async Task<IActionResult> Edit(int id, Project project)
   {
    if (id != project.Id) return NotFound();
      if (ModelState.IsValid)
            {
      _context.Update(project);
    await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
      ViewBag.Categories = new SelectList(_context.ProjectCategories.Where(c => c.IsActive).OrderBy(c => c.Order), "Id", "Title", project.CategoryId);
            return View(project);
}

        public async Task<IActionResult> Delete(int? id)
        {
  if (id == null) return NotFound();
   var project = await _context.Projects.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (project == null) return NotFound();
            return View(project);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
 {
            var project = await _context.Projects.FindAsync(id);
    if (project != null) _context.Projects.Remove(project);
         await _context.SaveChangesAsync();
 return RedirectToAction(nameof(Index));
        }
    }
}
