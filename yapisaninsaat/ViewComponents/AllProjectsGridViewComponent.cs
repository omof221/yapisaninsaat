using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class AllProjectsGridViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
  public AllProjectsGridViewComponent(AppDbContext context) => _context = context;

public async Task<IViewComponentResult> InvokeAsync(int? durum, int? kategori)
  {
     var query = _context.Projects
    .Where(p => p.IsActive)
     .Include(p => p.Category)
       .Include(p => p.ProjectImages)
   .AsQueryable();

    if (durum.HasValue)
         query = query.Where(p => p.ProjectStatus == durum.Value);

   if (kategori.HasValue)
   query = query.Where(p => p.CategoryId == kategori.Value);

    var projects = await query
    .OrderByDescending(p => p.IsFeatured)
       .ThenByDescending(p => p.CreatedDate)
        .ToListAsync();

     var categories = await _context.ProjectCategories
  .Where(c => c.IsActive)
    .OrderBy(c => c.Order)
 .ToListAsync();

   ViewBag.Durum = durum;
         ViewBag.Kategori = kategori;
         ViewBag.Categories = categories;

     return View(projects);
        }
    }
}
