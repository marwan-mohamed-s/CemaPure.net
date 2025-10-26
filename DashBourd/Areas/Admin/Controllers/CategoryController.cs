using DashBourd.Models;
using Microsoft.AspNetCore.Mvc;

namespace DashBourd.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IGenericRepository<Category> _repository;

        public CategoryController(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _repository.GetAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            await _repository.AddAsync(category);
            await _repository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _repository.GetOneAsync(c => c.Id == id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            var existing = await _repository.GetOneAsync(c => c.Id == category.Id);
            if (existing == null)
                return NotFound();

            existing.Name = category.Name;

            _repository.Update(existing);
            await _repository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _repository.GetOneAsync(c => c.Id == id);
            if (category != null)
            {
                _repository.Delete(category);
                await _repository.CommitAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
