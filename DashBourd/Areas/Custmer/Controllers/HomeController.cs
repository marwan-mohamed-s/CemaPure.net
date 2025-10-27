using Ecommerce1.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
