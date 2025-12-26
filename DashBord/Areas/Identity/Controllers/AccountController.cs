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
        private readonly IGenericRepository<ApplicationUserOTP> _ApplicationUserOTPsRepository;
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IGenericRepository<ApplicationUserOTP> applicationUserOTPsRepository
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _ApplicationUserOTPsRepository = applicationUserOTPsRepository;
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
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM forgetPasswordVM, CancellationToken cancellationToken)
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
                return View(forgetPasswordVM);
            }

            var user = await _userManager.FindByEmailAsync(forgetPasswordVM.UserNameOrEmail);
            if (user == null)
                user = await _userManager.FindByNameAsync(forgetPasswordVM.UserNameOrEmail);

            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "The user Name / Email  is not valid");
                TempData["ErrorMessage"] = "The user Name / Email  is not valid";
                return View(forgetPasswordVM);
            }


            var OTP = new Random().Next(1000, 9999);

            var UserOTPs = await _ApplicationUserOTPsRepository.GetAsync(e => e.ApplicationUserId == user.Id && e.IsValid == true);
            var TotalUserOTPs = UserOTPs.Count(e => (DateTime.UtcNow - e.CreateAt).TotalHours < 24);

            if(TotalUserOTPs > 5)
            {
                ModelState.AddModelError(String.Empty, "You have reached the maximum number of OTP requests for today. Please try again tomorrow.");
                TempData["ErrorMessage"] = "You have reached the maximum number of OTP requests for today. Please try again tomorrow (After 24h).";
                return View(forgetPasswordVM);
            }
            else
            {
                await _ApplicationUserOTPsRepository.AddAsync(new ApplicationUserOTP
                {
                    OTP = OTP.ToString(),
                    ApplicationUserId = user.Id,
                    CreateAt = DateTime.Now,
                    ValidTo = DateTime.Now.AddMinutes(30),
                    IsValid = true,
                    Id = Guid.NewGuid().ToString(),// Identity عشان هو مش 
                }, cancellationToken: cancellationToken);

                await _ApplicationUserOTPsRepository.CommitAsync();

                await _emailSender.SendEmailAsync(user.Email!, "Reset the new password!"
                    , $"<h1>Your OTP is : {OTP} to reset your password , Don’t Share it to any one !</h1>");

                TempData["SuccessMessage"]= "OTP has been sent to your email successfully! Please check your inbox";

                TempData["FromForgetPassword"] = Guid.NewGuid().ToString();

                return RedirectToAction("ValidateOTP" , new { userId = user.Id });
            }

        }

        public IActionResult ValidateOTP(String UserId)
        {

            if (TempData["FromForgetPassword"] == null)
            {
                TempData["ErrorMessage"] = "You are not authorized to access this page.";
                return RedirectToAction("Login");
            }

            return View(new ValidateOTPVM{
                ApplicationUserId = UserId
            });
        }
        [HttpPost]
        public async Task<IActionResult> ValidateOTP(ValidateOTPVM validateOTPVM)
        {

            if(!ModelState.IsValid)
            {
                return View(validateOTPVM);
            }

            var ValidOTP = await _ApplicationUserOTPsRepository.GetOneAsync(
            e => e.ApplicationUserId == validateOTPVM.ApplicationUserId
            && e.OTP == validateOTPVM.OTP && e.IsValid == true
            && e.ValidTo > DateTime.UtcNow
            );

            if(ValidOTP == null)
            {
                ModelState.AddModelError(String.Empty, "Invalid OTP");
                TempData["ErrorMessage"] = "Invalid OTP";
                return RedirectToAction("ValidateOTP", new { userId = validateOTPVM.ApplicationUserId });
            }

            //السطر ده يمنع انه يروح لصفحه التشاانج باسسوورد علطول لو عرف ااي دي حد مثلا
            //لازم ييجي من صفحه التحقق الاول
            //عشان الرساله دي تتكرييت و متكونش ناال
            //مهم!!!!
            TempData["FromValidateOTP"] = Guid.NewGuid().ToString();

            return RedirectToAction("ChangePassword" , new {userId = validateOTPVM.ApplicationUserId});
        }


        public IActionResult ChangePassword(String UserId)
        {
            //التحقق من انه جاي من صفحه التحقق مش جاي علطول
            if (TempData["FromValidateOTP"] == null)
            {
                TempData["ErrorMessage"] = "You are not authorized to access this page.";
                return RedirectToAction("Login");
            }

            return View( new ChangePasswordVM
            {
                ApplicationUserId = UserId
            });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(changePasswordVM);
            }

            var user = await _userManager.FindByIdAsync( changePasswordVM.ApplicationUserId);

            if(user == null)
            {
                ModelState.AddModelError(String.Empty, "Invalid User");
                TempData["ErrorMessage"] = "Invalid User";
                return View(changePasswordVM);
            }

            //way2
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, changePasswordVM.NewPassword);
            /////////////////////////////

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(changePasswordVM);
            }

            TempData["SuccessMessage"] = "Password Changed Successfully";

            //way2
            //await _userManager.RemovePasswordAsync(user);
            //var result = await _userManager.AddPasswordAsync(user, changePasswordVM.NewPassword);
            ///////////////////////////////

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

            if (user == null)
            {
                TempData["ErrorMessage"] = "The user Name / Email or password are not valid";
                //ModelState.AddModelError(String.Empty, "The user Name / Email or password are not valid");
                return View(loginVM);
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    TempData["ErrorMessage"] = "Your account has been locked out due to multiple failed login attempts. Please try again later.";
                }
                else if (!user.EmailConfirmed)
                {
                    TempData["ErrorMessage"] = "You must Confirm your Email First";
                }
                else
                {
                    TempData["ErrorMessage"] = "The user Name / Email or password are not valid";
                }
                return View(loginVM);
            }

            TempData["FromPageLogin"] = Guid.NewGuid().ToString();

            return RedirectToAction("Index", "Home", new { area = "Custmer" });

        }


    }
}
