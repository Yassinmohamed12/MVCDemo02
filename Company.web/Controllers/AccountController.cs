using Company.Data.Entites;
using Company.Services.Helper;
using Company.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = input.Email.Split("@")[0],
                    Email = input.Email,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    IsActive = true,
                };
                // Need to Create the User then you can use CreateAsync to make create and hash the password
                var result = await _userManager.CreateAsync(user, input.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("LogIn");
                }
                //If not Succeeded that result is return of list of errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(input);
        }

        [HttpGet]
        public IActionResult LogIn()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel logInViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(logInViewModel.UserName);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, logInViewModel.Password, logInViewModel.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return Redirect("/Home/Index");
                    }
                    else if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Password not Correct,Please Enter Password Again");

                        return View(logInViewModel);
                    }
                }

                if (user is null)
                {
                    ModelState.AddModelError("", "User not Found,PLease SignUp");

                    return View(logInViewModel);
                }

            }
            return View(logInViewModel);
        }

        public  new async Task<IActionResult> SignOut()
        {
              await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Email);

                if (user is not null)
                {
                    // Need to make token to send a link to my gmail

                    var token  = await _userManager.GeneratePasswordResetTokenAsync(user);


                    //make Url that send to the Gmail
                    var url = Url.Action("ResetPassword", "Account", new { Email = forgetPasswordViewModel.Email, Token = token }, Request.Scheme);

                    var email = new Email
                    {
                        Body = url,
                        Subject = "ResetPassword",
                        To = forgetPasswordViewModel.Email
                    };

                    EmailSettings.SendEmail(email);

                    return RedirectToAction(nameof(CheckYourInbox));

                }
            }
            return View(forgetPasswordViewModel);
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string Email,string Token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel input)
        {
            if (ModelState.IsValid) 
            {
                var user = await _userManager.FindByEmailAsync(input.Email);

                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user,input.Token,input.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(LogIn));
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            return View(input);
        }
    }
}
