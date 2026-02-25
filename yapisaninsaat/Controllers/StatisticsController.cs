using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly AppDbContext _context;
     public StatisticsController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index() => View(await _context.Statistics.OrderBy(s => s.Order).ToListAsync());
    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
  public async Task<IActionResult> Create(Statistic item)
      {
    if (ModelState.IsValid) { _context.Add(item); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
      return View(item);
   }

  public async Task<IActionResult> Edit(int? id)
        {
  if (id == null) return NotFound();
var item = await _context.Statistics.FindAsync(id);
if (item == null) return NotFound();
 return View(item);
  }

   [HttpPost, ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, Statistic item)
        {
  if (id != item.Id) return NotFound();
  if (ModelState.IsValid) { _context.Update(item); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
return View(item);
  }

     public async Task<IActionResult> Delete(int? id)
        {
  if (id == null) return NotFound();
        var item = await _context.Statistics.FindAsync(id);
if (item == null) return NotFound();
      return View(item);
    }

     [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
   {
   var item = await _context.Statistics.FindAsync(id);
   if (item != null) _context.Statistics.Remove(item);
            await _context.SaveChangesAsync();
       return RedirectToAction(nameof(Index));
  }
    }
}
