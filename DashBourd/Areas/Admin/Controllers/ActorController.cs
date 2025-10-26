using DashBourd.Models;
using Microsoft.AspNetCore.Mvc;

namespace DashBourd.Controllers
{
    [Area("Admin")]
    public class ActorController : Controller
    {
        private readonly IGenericRepository<Actor> _actorRepo;
        private readonly IWebHostEnvironment _env;

        public ActorController(IGenericRepository<Actor> actorRepo, IWebHostEnvironment env)
        {
            _actorRepo = actorRepo;
            _env = env;
        }

        // ✅ عرض كل الممثلين
        public async Task<IActionResult> Index()
        {
            var actors = await _actorRepo.GetAsync();
            return View(actors);
        }

        // ✅ صفحة الإضافة
        [HttpGet]
        public IActionResult Create() => View();

        // ✅ تنفيذ الإضافة
        [HttpPost]
        public async Task<IActionResult> Create(Actor actor, IFormFile? Image)
        {
            if (!ModelState.IsValid)
                return View(actor);

            if (Image != null && Image.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                var filePath = Path.Combine(_env.WebRootPath, "images/Actors", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await Image.CopyToAsync(stream);
                }

                actor.Image = fileName;
            }

            await _actorRepo.AddAsync(actor);
            await _actorRepo.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        // ✅ صفحة التعديل
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _actorRepo.GetOneAsync(a => a.Id == id);
            if (actor == null)
                return NotFound();

            return View(actor);
        }

        // ✅ تنفيذ التعديل
        [HttpPost]
        public async Task<IActionResult> Edit(Actor actor, IFormFile? Img)
        {
            var existing = await _actorRepo.GetOneAsync(a => a.Id == actor.Id);
            if (existing == null)
                return NotFound();

            existing.Name = actor.Name;

            if (Img != null && Img.Length > 0)
            {
                if (!string.IsNullOrEmpty(existing.Image))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, "images/Actors", existing.Image);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(Img.FileName);
                var filePath = Path.Combine(_env.WebRootPath, "images/Actors", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await Img.CopyToAsync(stream);
                }

                existing.Image = fileName;
            }

            _actorRepo.Update(existing);
            await _actorRepo.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        // ✅ حذف ممثل
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _actorRepo.GetOneAsync(a => a.Id == id);
            if (actor == null)
                return NotFound();

            if (!string.IsNullOrEmpty(actor.Image))
            {
                var path = Path.Combine(_env.WebRootPath, "images/Actors", actor.Image);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }

            _actorRepo.Delete(actor);
            await _actorRepo.CommitAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
