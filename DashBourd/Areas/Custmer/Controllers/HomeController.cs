using Ecommerce1.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DashBourd.Areas.Custmer.Controllers
{
    [Area("Custmer")]
    public class HomeController : Controller
    {

        ApplicationDbContext _context = new ApplicationDbContext();
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
    }
}
