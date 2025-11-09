using DashBourd.Models;
using Ecommerce1.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DashBourd.Areas.Custmer.Controllers
{
    [Area("Custmer")]
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // عرض تفاصيل الفيلم (الصفحة الأصلية)
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Category)
                .Include(m => m.Cinema)
                .Include(m => m.SubImages)
                .Include(m => m.MovieActors).ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null) return NotFound();

            return View(movie);
        }

        // صفحة الحجز
        public async Task<IActionResult> BookTicket(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Cinema)
                .FirstOrDefault(m => m.Id == id);
            if (movie == null) return NotFound();

            var bookedSeats = await _context.Bookings
                .Where(b => b.MovieId == id)
                .Select(b => b.SeatNumber)
                .ToListAsync();

            ViewBag.BookedSeats = bookedSeats;
            return View(movie);
        }

        // تأكيد الحجز
        [HttpPost]
        public async Task<IActionResult> ConfirmBooking(int movieId, string[] seats)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "يجب تسجيل الدخول أولاً";
                return RedirectToAction("BookTicket", new { id = movieId });
            }

            try
            {
                foreach (var seat in seats)
                {
                    var exists = await _context.Bookings
                        .AnyAsync(b => b.MovieId == movieId && b.SeatNumber == seat);

                    if (!exists)
                    {
                        _context.Bookings.Add(new Booking
                        {
                            MovieId = movieId,
                            UserId = userId,
                            SeatNumber = seat,
                            BookingDate = DateTime.Now
                        });
                    }
                }

                await _context.SaveChangesAsync();

                // تمرير البيانات لصفحة التأكيد
                ViewBag.MovieId = movieId;
                ViewBag.SelectedSeats = seats;

                return View("BookingConfirmed");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "فشل في الحجز: " + ex.Message;
                return RedirectToAction("BookTicket", new { id = movieId });
            }
        }
    }
}
