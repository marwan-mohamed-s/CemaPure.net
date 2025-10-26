using DashBourd.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DashBourd.Controllers
{
    [Area("Admin")]
    public class CinemaController : Controller
    {
        private readonly IGenericRepository<Cinema> _repository;

        public CinemaController(IGenericRepository<Cinema> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var cinemas = await _repository.GetAsync();
            return View(cinemas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Cinema());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cinema cinema, [Required] IFormFile Image)
        {
            if (!ModelState.IsValid)
                return View(cinema);

            if (Image != null && Image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Cinemas", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await Image.CopyToAsync(stream);
                }

                cinema.Image = fileName;
            }

            await _repository.AddAsync(cinema);
            await _repository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cinema = await _repository.GetOneAsync(c => c.Id == id);
            if (cinema == null)
                return NotFound();

            return View(cinema);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Cinema cinema, IFormFile Image)
        {
            var existingCinema = await _repository.GetOneAsync(c => c.Id == cinema.Id);
            if (existingCinema == null)
                return NotFound();

            existingCinema.Name = cinema.Name;

            if (Image != null && Image.Length > 0)
            {
                // حذف الصورة القديمة لو موجودة
                if (!string.IsNullOrEmpty(existingCinema.Image))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Cinemas", existingCinema.Image);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                // حفظ الصورة الجديدة
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Cinemas", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await Image.CopyToAsync(stream);
                }

                existingCinema.Image = fileName;
            }

            _repository.Update(existingCinema);
            await _repository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _repository.GetOneAsync(c => c.Id == id);
            if (cinema != null)
            {
                // حذف الصورة من الفولدر لو موجودة
                if (!string.IsNullOrEmpty(cinema.Image))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Cinemas", cinema.Image);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }

                _repository.Delete(cinema);
                await _repository.CommitAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
