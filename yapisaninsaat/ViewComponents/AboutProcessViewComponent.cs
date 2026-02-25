using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class AboutProcessViewComponent : ViewComponent
    {
     private readonly AppDbContext _context;
        public AboutProcessViewComponent(AppDbContext context) => _context = context;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var services = await _context.Services
  .Where(s => s.IsActive)
           .OrderBy(s => s.Order)
  .ToListAsync();
     return View(services);
  }
    }
}
