using Microsoft.AspNetCore.Mvc;

namespace DashBourd.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
