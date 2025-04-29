using ECARE.Constants;
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

        //[HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register()
        {
            //ViewBag.Labs = new SelectList(await _context.Lab.ToListAsync(), "Id", "LabName");
            var model = new RegisterViewModel
            {
                Labs = new SelectList(await _context.Lab.ToListAsync(), "Id", "LabName"),
                Pharmacies = new SelectList(await _context.Pharmacies.ToListAsync(), "Id", "Name")

            };
            return View(model);
        }
        [HttpPost]
        //TODO:Fix the trailing space at the end
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            model.Labs = new SelectList(await _context.Lab.ToListAsync(), "Id", "LabName");
            model.Pharmacies = new SelectList(await _context.Pharmacies.ToListAsync(), "Id", "Name");

            if (!ModelState.IsValid)
            {
                // ViewBag.Labs = new SelectList(await _context.Lab.ToListAsync(), "Id", "LabName");

                ModelState.AddModelError("", "All fields are required.");
                return View(model);
            }

            if (model.Role == AuthorizationConstants.LabAdmin && !model.LabId.HasValue)
            {
                ModelState.AddModelError("LabId", "Lab must be selected for Lab Admin.");
            }
            else if (model.Role == AuthorizationConstants.PharmacyAdmin && !model.PharmacyId.HasValue)
            {
                ModelState.AddModelError("PharmacyId", "Pharmacy must be selected for Pharmacy Admin.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if ((await _userManager.FindByNameAsync(model.Username) != null))
            {
                ModelState.AddModelError("UserName", "User name is already taken.");
                return View(model);
            }
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                LabId = model.Role == AuthorizationConstants.LabAdmin ? model.LabId : null,
                PharmacyId = model.Role == AuthorizationConstants.PharmacyAdmin ? model.PharmacyId : null
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                try
                {
                    await _userManager.AddToRoleAsync(user, model.Role);

                    return RedirectToAction("IndexAdmin", "PatientRegistrations");
                }
                catch (Exception ex)
                {
                    // Rollback user creation if role assignment fails
                    await _userManager.DeleteAsync(user);
                    ModelState.AddModelError("", $"Role assignment failed: {ex.Message}");
                    return View(model);
                }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
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

                if (roles.Contains(AuthorizationConstants.Admin))
                {
                    return RedirectToAction("Indexadmin", "PatientRegistrations");
                }
                else if (roles.Contains(AuthorizationConstants.LabUser) || roles.Contains(AuthorizationConstants.LabAdmin))
                {
                    return RedirectToAction("Index", "PatientRegistrations");
                }
                else if (roles.Contains(AuthorizationConstants.PharmacyAdmin))
                {
                    return RedirectToAction("Index", "Pharmacy");
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
