using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.Controllers
{
    public class PagesController : Controller
    {
        private readonly AppDbContext _context;

  public PagesController(AppDbContext context)
        {
      _context = context;
        }

    public async Task<IActionResult> Index()
        {
            return View(await _context.Pages.OrderByDescending(p => p.CreatedDate).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
      if (id == null) return NotFound();
            var page = await _context.Pages.FindAsync(id);
          if (page == null) return NotFound();
      return View(page);
}

        public IActionResult Create()
        {
        return View();
    }

        [HttpPost]
   [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
 {
            if (ModelState.IsValid)
    {
              page.CreatedDate = DateTime.Now;
                _context.Add(page);
   await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
     var page = await _context.Pages.FindAsync(id);
       if (page == null) return NotFound();
     return View(page);
        }

  [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Page page)
 {
            if (id != page.Id) return NotFound();
 if (ModelState.IsValid)
         {
    page.UpdatedDate = DateTime.Now;
      _context.Update(page);
         await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        public async Task<IActionResult> Delete(int? id)
        {
      if (id == null) return NotFound();
         var page = await _context.Pages.FindAsync(id);
            if (page == null) return NotFound();
    return View(page);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
    var page = await _context.Pages.FindAsync(id);
     if (page != null) _context.Pages.Remove(page);
            await _context.SaveChangesAsync();
return RedirectToAction(nameof(Index));
    }
    }
}
