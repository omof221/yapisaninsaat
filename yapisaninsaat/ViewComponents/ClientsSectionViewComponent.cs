using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class ClientsSectionViewComponent : ViewComponent
    {
    private readonly AppDbContext _context;
  public ClientsSectionViewComponent(AppDbContext context) => _context = context;

  public async Task<IViewComponentResult> InvokeAsync()
  {
  var refs = await _context.References
    .Where(r => r.IsActive)
    .OrderBy(r => r.Order)
 .ToListAsync();
     return View(refs);
  }
    }
}
