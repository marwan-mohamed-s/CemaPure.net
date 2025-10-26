using DashBourd.Models;
using Ecommerce1.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DashBourd.Controllers
{
    [Area("Admin")]
    public class CinemaController : Controller
    {
        ApplicationDbContext _Context = new ApplicationDbContext();

        public IActionResult Index()
        {
            var cinemas = _Context.Cinemas.ToList();
            return View(cinemas);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View(new Cinema());
        }

        [HttpPost]
        public IActionResult Create(Cinema cinema,[Required] IFormFile Image)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }

            if (Image != null && Image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Cinemas", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    Image.CopyTo(stream);
                }

                cinema.Image = fileName;
            }

            _Context.Cinemas.Add(cinema);
            _Context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cinema = _Context.Cinemas.FirstOrDefault(c => c.Id == id);
            if (cinema == null)
                return NotFound();

            return View(cinema);
        }

        [HttpPost]
        public IActionResult Edit(Cinema cinema, IFormFile Image)
        {
            var existingCinema = _Context.Cinemas.FirstOrDefault(c => c.Id == cinema.Id);
            if (existingCinema == null)
                return NotFound();

            existingCinema.Name = cinema.Name;

            if (Image != null && Image.Length > 0)
            {
                if (!string.IsNullOrEmpty(existingCinema.Image))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Cinemas", existingCinema.Image);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Cinemas", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    Image.CopyTo(stream);
                }

                existingCinema.Image = fileName;
            }

            _Context.Cinemas.Update(existingCinema);
            _Context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var cinema = _Context.Cinemas.FirstOrDefault(c => c.Id == id);
            if (cinema != null)
            {
                if (!string.IsNullOrEmpty(cinema.Image))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Cinemas", cinema.Image);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }

                _Context.Cinemas.Remove(cinema);
                _Context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
