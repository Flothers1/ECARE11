using ECARE.Models;
using ECARE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECARE.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ECAREContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ECAREContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register()
        {
            //ViewBag.Labs = new SelectList(await _context.Lab.ToListAsync(), "Id", "LabName");
            var model = new RegisterViewModel
            {
                Labs = new SelectList(await _context.Lab.ToListAsync(), "Id", "LabName")
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            model.Labs = new SelectList(await _context.Lab.ToListAsync(), "Id", "LabName");

            if (!ModelState.IsValid)
            {
                // ViewBag.Labs = new SelectList(await _context.Lab.ToListAsync(), "Id", "LabName");
                ModelState.AddModelError("", "All fields are required.");
                return View(model);
            }

            ApplicationUser user = null;

            //if (model.Role == "LabUser" && !model.LabBranchId.HasValue)
            //{
            //    ModelState.AddModelError("LabBranchId", "Lab branch must be selected for LabUser.");
            //    return View(model);
            //}
            //else
            //{
            //    user = new ApplicationUser { UserName = model.Username, Email = model.Email, LabBranchId = model.LabBranchId.Value };
            //}
            if (model.Role == "LabAdmin" && !model.LabId.HasValue)
            {
                ModelState.AddModelError("LabId", "Lab must be selected for LabAdmin.");
                return View(model);
            }
            else
            {
                user = new ApplicationUser { UserName = model.Username, Email = model.Email, LabId = model.LabId.Value };

            }
            if((await _userManager.FindByNameAsync(model.Username) != null))
            {
                ModelState.AddModelError("UserName", "Username is already taken.");
                return View(model); 
            }
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);

                return RedirectToAction("Indexadmin", "PatientRegistrations");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }



        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Username and password are required.");
                return View();
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("Indexadmin", "PatientRegistrations");
                }
                else if (roles.Contains("LabUser") || roles.Contains("LabAdmin"))
                {
                    return RedirectToAction("Index", "PatientRegistrations");
                }
                

                return RedirectToAction("Index", "Home"); 
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account"); 
        }
    }

}
