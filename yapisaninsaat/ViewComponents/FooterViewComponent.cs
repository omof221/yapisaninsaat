using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
  private readonly AppDbContext _context;
        public FooterViewComponent(AppDbContext context) => _context = context;

  public async Task<IViewComponentResult> InvokeAsync()
   {
        var settings = await _context.Settings.FirstOrDefaultAsync();
        var services = await _context.Services.Where(s => s.IsActive).OrderBy(s => s.Order).Take(4).ToListAsync();
         ViewBag.Services = services;
    return View(settings);
 }
    }
}
