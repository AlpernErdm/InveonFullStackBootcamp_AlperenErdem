using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAutomation.Models;

namespace WebAutomation.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new AppRole { Name = roleName });

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Update(AppRole role)
        {
            if (ModelState.IsValid)
            {
                var existingRole = await _roleManager.FindByIdAsync(role.Id);
                if (existingRole == null)
                    return NotFound();

                existingRole.Name = role.Name;

                var result = await _roleManager.UpdateAsync(existingRole);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(role);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            return View(role);
        }

     
    }
}
