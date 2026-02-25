using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
  private readonly AppDbContext _context;
        public NavbarViewComponent(AppDbContext context) => _context = context;

        public async Task<IViewComponentResult> InvokeAsync()
        {
 var settings = await _context.Settings.FirstOrDefaultAsync();
            var services = await _context.Services.Where(s => s.IsActive).OrderBy(s => s.Order).Take(6).ToListAsync();
            ViewBag.Settings = settings;
            ViewBag.Services = services;
      return View();
   }
    }
}
