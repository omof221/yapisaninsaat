using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.Controllers
{
    public class ProjectCategoriesController : Controller
    {
  private readonly AppDbContext _context;
    public ProjectCategoriesController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index() =>
    View(await _context.ProjectCategories.OrderBy(c => c.Order).ToListAsync());

        public async Task<IActionResult> Details(int? id)
 {
            if (id == null) return NotFound();
            var cat = await _context.ProjectCategories.Include(c => c.Projects).FirstOrDefaultAsync(c => c.Id == id);
         if (cat == null) return NotFound();
        return View(cat);
  }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectCategory category)
 {
            if (ModelState.IsValid)
        {
             _context.Add(category);
              await _context.SaveChangesAsync();
       return RedirectToAction(nameof(Index));
  }
        return View(category);
        }

 public async Task<IActionResult> Edit(int? id)
     {
            if (id == null) return NotFound();
   var cat = await _context.ProjectCategories.FindAsync(id);
    if (cat == null) return NotFound();
return View(cat);
        }

[HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProjectCategory category)
      {
 if (id != category.Id) return NotFound();
   if (ModelState.IsValid)
    {
  _context.Update(category);
                await _context.SaveChangesAsync();
       return RedirectToAction(nameof(Index));
            }
return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
       if (id == null) return NotFound();
   var cat = await _context.ProjectCategories.FindAsync(id);
       if (cat == null) return NotFound();
  return View(cat);
     }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
    {
          var cat = await _context.ProjectCategories.FindAsync(id);
            if (cat != null) _context.ProjectCategories.Remove(cat);
    await _context.SaveChangesAsync();
 return RedirectToAction(nameof(Index));
        }
    }
}
