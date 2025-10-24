using Microsoft.AspNetCore.Mvc;

namespace DashBourd.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
