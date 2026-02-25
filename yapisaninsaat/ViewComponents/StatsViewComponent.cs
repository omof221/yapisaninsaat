using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class StatsViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
 public StatsViewComponent(AppDbContext context) => _context = context;

      public async Task<IViewComponentResult> InvokeAsync()
        {
   var stats = await _context.Statistics
       .Where(s => s.IsActive)
         .OrderBy(s => s.Order)
            .ToListAsync();
     return View(stats);
        }
    }
}
