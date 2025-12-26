using ECommerce.Utitlies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DashBourd.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        [Authorize(Roles = $"{SD.ADMIN_ROLE},{SD.SUPER_ADMIN_ROLE},{SD.EMPLOYEE_ROLE}")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
