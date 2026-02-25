using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class TestimonialsSectionViewComponent : ViewComponent
    {
     private readonly AppDbContext _context;
        public TestimonialsSectionViewComponent(AppDbContext context) => _context = context;

   public async Task<IViewComponentResult> InvokeAsync()
        {
    var testimonials = await _context.Testimonials
    .Where(t => t.IsActive)
         .OrderBy(t => t.Order)
  .Take(4)
     .ToListAsync();
 return View(testimonials);
        }
    }
}
