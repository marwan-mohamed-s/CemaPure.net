using DashBourd.Models;
using DashBourd.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Threading.Tasks;

namespace DashBourd.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager, IEmailSender emailSender) {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
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

            var user = new ApplicationUser()
            {
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Email = registerVM.Email,
                UserName = registerVM.UserName,
            };

            var result = await _userManager.CreateAsync(user, registerVM.Password);


            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerVM);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action(nameof(ConfirmEmail), "Account", new { area = "Identity", token, userId = user.Id }, Request.Scheme);

            await _emailSender.SendEmailAsync(registerVM.Email, "Ecommerce 519 - Confirm Your Email!"
                , $"<h1>Confirm Your Email By Clicking <a href='{link}'>Here</a></h1>");

            TempData["SuccessMessage"] = "Create Account Successfully , Confierm your Email by Check Your box";

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                TempData["ErrorMessage"] = "Invalid User Cred.";

            var result = await _userManager.ConfirmEmailAsync(user!, token);

            if (!result.Succeeded)
                TempData["ErrorMessage"] = "Invalid OR Expired Token";
            else
                TempData["SuccessMessage"] = "Confirm Email Successfully";

            return RedirectToAction("Login");
        }


        public IActionResult ResendConfiremEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResendConfiremEmail(ResendEmailConfiremVM resendEmailConfiremVM)
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
                return View(resendEmailConfiremVM);
            }

            var user = await _userManager.FindByEmailAsync(resendEmailConfiremVM.UserNameOrEmail);
            if (user == null)
                user = await _userManager.FindByNameAsync(resendEmailConfiremVM.UserNameOrEmail);

            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "The user Name / Email  is not valid");
                TempData["ErrorMessage"] = "The user Name / Email  is not valid";
                return View(resendEmailConfiremVM);
            }

            if (user.EmailConfirmed)
            {
                ModelState.AddModelError(String.Empty, "Your Email is already confirmed");
                TempData["ErrorMessage"] = "Your Email is already confirmed";
                return View(resendEmailConfiremVM);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action(nameof(ConfirmEmail), "Account", new { area = "Identity", token, userId = user.Id }, Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email!, "Confirm Your Email!"
                , $"<h1>Confirm Your Email By Clicking <a href='{link}'>Here</a></h1>");

            TempData["SuccessMessage"] = "Confirmation email has been resent successfully! Please check your inbox";

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
