using Ecommerce1.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace DashBourd.Areas.Custmer.Controllers
{
    [Area("Custmer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            if (TempData["FromPageLogin"] == null)
            {
                TempData["ErrorMessage"] = "You are not authorized to access this page please Login.";
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var movies = _context.Movies
                .Include(m => m.Category)
                .ToList();

            var categories = _context.Categories
                .OrderBy(c => c.Name)
                .ToList();

            ViewBag.Categories = categories;
            return View(movies);
        }

        public IActionResult Details(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Category)
                .Include(m => m.Cinema)
                .Include(m => m.MovieActors)
                    .ThenInclude(ma => ma.Actor)
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return View(movie);
        }

    }
}
