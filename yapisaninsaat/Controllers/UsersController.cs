using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;
using BC = BCrypt.Net.BCrypt;

namespace yapisaninsaat.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index() =>
            View(await _context.Users.OrderByDescending(u => u.CreatedDate).ToListAsync());

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User item)
        {
            if (ModelState.IsValid)
            {
                item.PasswordHash = BC.HashPassword(item.PasswordHash);
                item.CreatedDate = DateTime.Now;
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.Users.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User item)
        {
            if (id != item.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var existing = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
                if (existing == null) return NotFound();

                if (!string.IsNullOrEmpty(item.PasswordHash) && item.PasswordHash != existing.PasswordHash)
                    item.PasswordHash = BC.HashPassword(item.PasswordHash);
                else
                    item.PasswordHash = existing.PasswordHash;

                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.Users.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Users.FindAsync(id);
            if (item != null) _context.Users.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}