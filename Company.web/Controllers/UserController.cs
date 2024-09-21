using Company.Data.Entites;
using Company.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Company.web.Controllers
{
    //if put Authorize only it return to the path in the configure cookie
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public UserController(UserManager<ApplicationUser> userManager,ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<IActionResult> Index(string SearchInp)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();

            if (string.IsNullOrEmpty(SearchInp))
            {
                users = await _userManager.Users.ToListAsync();
            }
            else
            {
                users = await _userManager.Users
                    .Where(user => user.NormalizedEmail.Trim().Contains(SearchInp.Trim().ToUpper()))
                    .ToListAsync();
            }
            return View(users);
        }
        public  async Task<IActionResult> Details(string? id,string viewname = "Details")
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null) 
                return NotFound();
            if(viewname == "Update")
            {
                var userviewmodel = new UserUpdateViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                };

                return View(viewname,userviewmodel);
            }

            return View(viewname,user);
        }
        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id,"Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string? id,UserUpdateViewModel applicationUser)
        {
            if(applicationUser.Id != id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var users = await _userManager.FindByIdAsync(id);

                if(users is null)
                {
                    return NotFound();
                }
                users.UserName = applicationUser.UserName;
                users.NormalizedUserName = applicationUser.UserName.ToUpper();

                var result = await _userManager.UpdateAsync(users);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User Update Successfully");
                    return RedirectToAction("Index");
                }
                foreach (var user in result.Errors)
                {
                    _logger.LogError(user.Description);
                }
            }
            return View(applicationUser);
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {

                if (id is null)
                {
                    return NotFound();
                }
                var user = await _userManager.FindByIdAsync(id);

                if (user is null)
                    return NotFound();

                user.IsDeleted = true;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User Delete Successfully");

                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    _logger.LogError(item.Description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}
