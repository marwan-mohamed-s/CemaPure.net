using DashBourd.Models;
using DashBourd.ViewModel;
using Ecommerce1.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DashBourd.Controllers
{
    public class MovieController : Controller
    {
        private readonly ILogger<MovieController> _logger;
        private readonly ApplicationDbContext _context;

        public MovieController(ILogger<MovieController> logger)
        {
            _logger = logger;
            _context = new ApplicationDbContext();
        }

        public IActionResult Index()
        {

            var movies = _context.Movies
                .Include(m => m.Category)
                .Include(m => m.Cinema)
                .ToList();

            return View(movies);
        }

        [HttpGet]
        public IActionResult Create()
        {


            var cinemas = _context.Cinemas.ToList();
            var categories = _context.Categories.ToList();
            var actors = _context.Actors.ToList();

            var movieVM = new MovieVM
            {
                Categories = categories,
                Cinemas = cinemas
            };

            ViewBag.ActorsList = actors.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();

            return View(movieVM);
        }

        [HttpPost]
        public IActionResult Create(Movie movie, IFormFile Img, List<IFormFile> SubImgs, List<int> Actors)
        {
            if(!ModelState.IsValid)
            {
                return View(movie);
            }


            if (Img != null && Img.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    Img.CopyTo(stream);
                }

                movie.MainImage = fileName;
            }

            if (SubImgs != null && SubImgs.Count > 0)
            {
                movie.SubImages = new List<string>();

                foreach (var subImg in SubImgs)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(subImg.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/MovieSubImg", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        subImg.CopyTo(stream);
                    }

                    movie.SubImages.Add(fileName);
                }
            }

            _context.Movies.Add(movie);
            _context.SaveChanges();

            if (Actors != null && Actors.Count > 0)
            {
                foreach (var actorId in Actors)
                {
                    var movieActor = new MovieActor
                    {
                        MovieId = movie.Id,
                        ActorId = actorId
                    };
                    _context.MovieActors.Add(movieActor);
                }
                _context.SaveChanges();
            }
            return RedirectToAction("Index");

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

            TempData["success"] = "Saved successfully";
            return View(movie);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Cinemas = _context.Cinemas.ToList();
            ViewBag.ActorsList = _context.Actors.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();

            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie, IFormFile Img, List<IFormFile> SubImgs)
        {
            var existingMovie = _context.Movies.FirstOrDefault(m => m.Id == movie.Id);
            if (existingMovie == null)
                return NotFound();

            existingMovie.Name = movie.Name;
            existingMovie.Description = movie.Description;
            existingMovie.Price = movie.Price;
            existingMovie.CinemaId = movie.CinemaId;
            existingMovie.CategoryId = movie.CategoryId;

            if (Img != null && Img.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    Img.CopyTo(stream);
                }

                existingMovie.MainImage = fileName;
            }

            if (SubImgs != null && SubImgs.Count > 0)
            {
                existingMovie.SubImages = new List<string>();

                foreach (var subImg in SubImgs)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(subImg.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/MovieSubImg", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        subImg.CopyTo(stream);
                    }

                    existingMovie.SubImages.Add(fileName);
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            TempData["success"] = "Movie deleted successfully!";
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
