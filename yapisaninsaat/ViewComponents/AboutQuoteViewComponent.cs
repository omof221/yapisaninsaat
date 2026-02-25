using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class AboutQuoteViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public AboutQuoteViewComponent(AppDbContext context) => _context = context;

        public async Task<IViewComponentResult> InvokeAsync()
        {
     var testimonial = await _context.Testimonials
  .Where(t => t.IsActive)
     .OrderBy(t => t.Order)
       .FirstOrDefaultAsync();
      return View(testimonial);
 }
    }
}
