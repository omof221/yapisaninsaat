using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.Controllers
{
    public class FAQsController : Controller
    {
   private readonly AppDbContext _context;
        public FAQsController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index() => View(await _context.FAQs.OrderBy(f => f.Order).ToListAsync());
      public IActionResult Create() => View();

  [HttpPost, ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(FAQ item)
        {
   if (ModelState.IsValid) { _context.Add(item); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
     return View(item);
   }

     public async Task<IActionResult> Edit(int? id)
  {
if (id == null) return NotFound();
 var item = await _context.FAQs.FindAsync(id);
   if (item == null) return NotFound();
      return View(item);
  }

    [HttpPost, ValidateAntiForgeryToken]
   public async Task<IActionResult> Edit(int id, FAQ item)
   {
  if (id != item.Id) return NotFound();
      if (ModelState.IsValid) { _context.Update(item); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
        return View(item);
  }

   public async Task<IActionResult> Delete(int? id)
  {
  if (id == null) return NotFound();
var item = await _context.FAQs.FindAsync(id);
 if (item == null) return NotFound();
     return View(item);
  }

   [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
 var item = await _context.FAQs.FindAsync(id);
     if (item != null) _context.FAQs.Remove(item);
  await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
   }
    }
}
