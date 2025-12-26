using DashBourd.Models;
using Ecommerce1.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DashBourd.Areas.Custmer.Controllers
{
    [Area("Custmer")]
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
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

        [HttpPost]
        public async Task<IActionResult> ConfirmBooking(int movieId, string[] seats)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "يجب تسجيل الدخول أولاً";
                return RedirectToAction("BookTicket", new { id = movieId });
            }

            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
            if (movie == null) return NotFound();

            try
            {
                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<Stripe.Checkout.SessionLineItemOptions>
            {
                new Stripe.Checkout.SessionLineItemOptions
                {
                    PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                    {
                        Currency = "egp",
                        ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                        {
                            Name = movie.Name,
                            Description = $"Seats: {string.Join(", ", seats)}"
                        },
                        UnitAmount = (long)(movie.Price * 100),
                    },
                    Quantity = seats.Length
                }
            },
                    Mode = "payment",

                    SuccessUrl = $"{Request.Scheme}://{Request.Host}/custmer/movies/BookingSuccess?movieId={movieId}&seats={string.Join(",", seats)}",
                    CancelUrl = $"{Request.Scheme}://{Request.Host}/custmer/movies/BookingCancel?movieId={movieId}"
                };

                var service = new Stripe.Checkout.SessionService();
                var session = service.Create(options);

                return Redirect(session.Url);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "فشل في بدء عملية الدفع: " + ex.Message;
                return RedirectToAction("BookTicket", new { id = movieId });
            }
        }


        public async Task<IActionResult> BookingSuccess(int movieId, string seats)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("BookTicket", new { id = movieId });

            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
            if (movie == null) return NotFound();

            var seatList = seats.Split(',', StringSplitOptions.RemoveEmptyEntries);

            foreach (var seat in seatList)
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

            ViewBag.Movie = movie;
            ViewBag.MovieId = movieId;
            ViewBag.SelectedSeats = seatList;
            return View();
        }


        public IActionResult BookingCancel(int movieId)
        {
            ViewBag.Movie = _context.Movies.Find(movieId);
            return View();
        }


    }
}
