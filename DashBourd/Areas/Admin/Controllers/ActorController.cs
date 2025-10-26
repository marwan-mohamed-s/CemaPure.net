using DashBourd.Models;
using Ecommerce1.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DashBourd.Controllers
{
    [Area("Admin")]
    public class ActorController : Controller
    {
        ApplicationDbContext _Context = new ApplicationDbContext();

        public IActionResult Index()
        {
            var actors = _Context.Actors.ToList();
            return View(actors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Actor actor,IFormFile Image)
        {

            //foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            //{
            //    Console.WriteLine(error.ErrorMessage);
            //}

            if (!ModelState.IsValid)
            {
               
                return View(actor);
            }

            if (Image != null && Image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Actors", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    Image.CopyTo(stream);
                }

                actor.Image = fileName;
            }

            _Context.Actors.Add(actor);
            _Context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var actor = _Context.Actors.FirstOrDefault(a => a.Id == id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        [HttpPost]
        public IActionResult Edit(Actor actor, IFormFile Img)
        {
            var existingActor = _Context.Actors.FirstOrDefault(a => a.Id == actor.Id);
            if (existingActor == null)
            {
                return NotFound();
            }

            existingActor.Name = actor.Name;

            if (Img != null && Img.Length > 0)
            {
                if (!string.IsNullOrEmpty(existingActor.Image))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Actors", existingActor.Image);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Actors", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    Img.CopyTo(stream);
                }

                existingActor.Image = fileName;
            }

            _Context.Actors.Update(existingActor);
            _Context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var actor = _Context.Actors.FirstOrDefault(a => a.Id == id);
            if (actor != null)
            {
                if (!string.IsNullOrEmpty(actor.Image))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Actors", actor.Image);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }

                _Context.Actors.Remove(actor);
                _Context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
