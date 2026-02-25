using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

namespace yapisaninsaat.ViewComponents
{
    public class AboutTeamViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
     public AboutTeamViewComponent(AppDbContext context) => _context = context;

        public async Task<IViewComponentResult> InvokeAsync()
   {
    var team = await _context.TeamMembers
       .Where(t => t.IsActive)
   .OrderBy(t => t.Order)
    .ToListAsync();
  return View(team);
 }
    }
}
