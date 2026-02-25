using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.Controllers
{
    public class ContactMessagesController : Controller
    {
        private readonly AppDbContext _context;
  public ContactMessagesController(AppDbContext context) => _context = context;

      public async Task<IActionResult> Index() =>
  View(await _context.ContactMessages.OrderByDescending(c => c.CreatedDate).ToListAsync());

        public async Task<IActionResult> Details(int? id)
 {
      if (id == null) return NotFound();
   var item = await _context.ContactMessages.FindAsync(id);
   if (item == null) return NotFound();
if (!item.IsRead) { item.IsRead = true; await _context.SaveChangesAsync(); }
    return View(item);
        }

    public IActionResult Create() => View();

     [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ContactMessage item)
    {
if (ModelState.IsValid) { item.CreatedDate = DateTime.Now; _context.Add(item); await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
  return View(item);
 }

   public async Task<IActionResult> Delete(int? id)
{
   if (id == null) return NotFound();
 var item = await _context.ContactMessages.FindAsync(id);
       if (item == null) return NotFound();
         return View(item);
        }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
        {
       var item = await _context.ContactMessages.FindAsync(id);
    if (item != null) _context.ContactMessages.Remove(item);
     await _context.SaveChangesAsync();
  return RedirectToAction(nameof(Index));
        }
    }
}
