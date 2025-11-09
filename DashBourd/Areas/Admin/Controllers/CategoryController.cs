using DashBourd.Models;
using ECommerce.Utitlies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DashBourd.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.ADMIN_ROLE},{SD.SUPER_ADMIN_ROLE},{SD.EMPLOYEE_ROLE}")]

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
        [Authorize(Roles = $"{SD.ADMIN_ROLE},{SD.SUPER_ADMIN_ROLE}")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{SD.ADMIN_ROLE},{SD.SUPER_ADMIN_ROLE}")]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            await _repository.AddAsync(category);
            await _repository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = $"{SD.ADMIN_ROLE},{SD.SUPER_ADMIN_ROLE}")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _repository.GetOneAsync(c => c.Id == id);
            if (category == null)
                return NotFound();

            return View(category);
        }
        [HttpPost]
        [Authorize(Roles = $"{SD.ADMIN_ROLE},{SD.SUPER_ADMIN_ROLE}")]
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
        [Authorize(Roles = $"{SD.ADMIN_ROLE},{SD.SUPER_ADMIN_ROLE}")]
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
