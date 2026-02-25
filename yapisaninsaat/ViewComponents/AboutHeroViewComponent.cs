using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
  public class AboutHeroViewComponent : ViewComponent
    {
  private readonly AppDbContext _context;
        public AboutHeroViewComponent(AppDbContext context) => _context = context;

        public async Task<IViewComponentResult> InvokeAsync()
        {
     var settings = await _context.Settings.FirstOrDefaultAsync();
       var stats = await _context.Statistics.Where(s => s.IsActive).OrderBy(s => s.Order).Take(3).ToListAsync();
     var services = await _context.Services.Where(s => s.IsActive).OrderBy(s => s.Order).ToListAsync();

      ViewBag.Settings = settings;
   ViewBag.Stats = stats;
     ViewBag.Services = services;
   return View(settings);
        }
    }
}
