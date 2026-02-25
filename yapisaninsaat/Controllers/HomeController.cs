using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using yapisaninsaat.Models;
using Microsoft.EntityFrameworkCore;

namespace yapisaninsaat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var settings = await _context.Settings.FirstOrDefaultAsync();
            ViewBag.Settings = settings;
            return View();
        }

        public async Task<IActionResult> About()
        {
            var settings = await _context.Settings.FirstOrDefaultAsync();
            ViewBag.Settings = settings;
            return View();
        }

        public async Task<IActionResult> Projects(int? durum, int? kategori)
        {
            var settings = await _context.Settings.FirstOrDefaultAsync();
            ViewBag.Settings = settings;
            ViewBag.Durum = durum;
            ViewBag.Kategori = kategori;
            return View();
        }

        public async Task<IActionResult> ProjectDetail(string slug)
        {
            if (string.IsNullOrEmpty(slug)) return NotFound();

            var project = await _context.Projects
                .Include(p => p.Category)
                .Include(p => p.ProjectImages.OrderBy(pi => pi.Order))
                .FirstOrDefaultAsync(p => p.Slug == slug && p.IsActive);

            // Eski slug ile de arama yap
            if (project == null)
            {
                project = await _context.Projects
                    .Include(p => p.Category)
                    .Include(p => p.ProjectImages.OrderBy(pi => pi.Order))
                    .FirstOrDefaultAsync(p => p.OldSlugs != null && p.OldSlugs.Contains(slug) && p.IsActive);

                if (project != null)
                    return RedirectToActionPermanent(nameof(ProjectDetail), new { slug = project.Slug });
            }

            if (project == null) return NotFound();

            var settings = await _context.Settings.FirstOrDefaultAsync();
            ViewBag.Settings = settings;
            ViewBag.Project = project;

            return View(project);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
