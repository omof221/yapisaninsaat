using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class FaqSectionViewComponent : ViewComponent
    {
       private readonly AppDbContext _context;
        public FaqSectionViewComponent(AppDbContext context) => _context = context;

   public async Task<IViewComponentResult> InvokeAsync()
        {
     var faqs = await _context.FAQs
        .Where(f => f.IsActive)
       .OrderBy(f => f.Order)
   .ToListAsync();
   return View(faqs);
        }
  }
}
