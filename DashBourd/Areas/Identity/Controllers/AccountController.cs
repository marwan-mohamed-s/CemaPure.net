using DashBourd.Models;
using DashBourd.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DashBourd.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }

                return View(registerVM);
            }

            var result = await _userManager.CreateAsync(new()
            {
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Email = registerVM.Email,
                UserName = registerVM.UserName,
            }, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerVM);
            }

            return RedirectToAction("Login");
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.UsernameOrEmail) ?? await _userManager.FindByNameAsync(loginVM.UsernameOrEmail);

            if(user == null)
            {
                ModelState.AddModelError(String.Empty, "The user Name / Email or password are not valid");
                return View(loginVM);
            }

            var result =await _signInManager.PasswordSignInAsync(user, loginVM.Password,loginVM.RememberMe,lockoutOnFailure:true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(String.Empty, "you have done your 5 times to login , please try again after 5 min");
                }
                else if (!user.EmailConfirmed)
                {
                    ModelState.AddModelError(String.Empty, "You must Confirm your Email First");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "The user Name / Email or password are not valid");
                }
                return View(loginVM);
            }

            return RedirectToAction("Index", "Home", new { area = "Custmer" });

        }


    }
}
