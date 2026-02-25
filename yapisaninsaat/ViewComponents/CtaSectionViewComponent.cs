using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class CtaSectionViewComponent : ViewComponent
    {
  private readonly AppDbContext _context;
     public CtaSectionViewComponent(AppDbContext context) => _context = context;

    public async Task<IViewComponentResult> InvokeAsync()
  {
         var settings = await _context.Settings.FirstOrDefaultAsync();
         return View(settings);
        }
    }
}
