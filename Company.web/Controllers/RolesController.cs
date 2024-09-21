using Company.Data.Entites;
using Company.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Company.web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RolesController> _logger;

        public RolesController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,ILogger<RolesController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var role = await _roleManager.Roles.ToListAsync();

            return View(role);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            if(ModelState.IsValid)
            {

                var result = await _roleManager.CreateAsync(role);

                if(result.Succeeded)
                {
                    _logger.LogInformation("ADD ROLE Successfully");
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    _logger.LogError(item.Description);
                }
            }
            return View(role);
        }

        public async Task<IActionResult> Details(string? id,string viewname = "Details")
        {
            var role = await _roleManager.FindByIdAsync(id);

            if(role == null)
                return NotFound();
            if(viewname == "Update")
            {
                var roleViewModel = new RoleUpdateViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                };
                return View(viewname, roleViewModel);
            }
            return View(viewname,role);
        }
        [HttpGet]
        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id, "Update");
        }

        public async Task<IActionResult> Update(string id,RoleUpdateViewModel role)
        {

            if(ModelState.IsValid)
            {
                var roles = await _roleManager.FindByIdAsync(id);

                if(roles is null)
                    return NotFound();

                roles.Name = role.Name;

                var result = await _roleManager.UpdateAsync(roles);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Update Sucess");
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                {
                    _logger.LogError(item.Description);
                }
            }
            return View(role);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if(id is null) 
                return NotFound();

            var role = await _roleManager.FindByIdAsync(id);

            if(role is null)
            {
                return NotFound();
            }
            
            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                _logger.LogInformation($"{role.Name} deleted");

                return RedirectToAction("Index");
            }
            foreach (var item in result.Errors)
            {
                _logger.LogError(item.Description);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string roleid)
        {
            var role = await _roleManager.FindByIdAsync(roleid);
            if (role is null)
                return NotFound();

            ViewBag.RoleId = roleid;

            var users = await _userManager.Users.ToListAsync();

            var ListOfUsers = new List<UserInRoleViewModel>();

            foreach (var user in users)
            {
                var userInRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userInRole.IsSelected = true;
                else
                    userInRole.IsSelected = false;

                ListOfUsers.Add(userInRole);
            }
            return View(ListOfUsers);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleid, List<UserInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleid);

            if (role is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appuser = await _userManager.FindByIdAsync(user.UserId);

                    if(appuser is not null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appuser, role.Name))
                            await _userManager.AddToRoleAsync(appuser, role.Name);
                        else if(!user.IsSelected && await _userManager.IsInRoleAsync(appuser,role.Name))
                            await _userManager.RemoveFromRoleAsync(appuser, role.Name);
                    } 
                }
                return RedirectToAction("Update", new { id = roleid });
            }
            return View(users);
        }
    }
}
